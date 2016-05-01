using System.Runtime.InteropServices;
using System.Windows.Forms;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System.Collections.Generic;

namespace MatchuPicchu
{

    [ComVisible(true)]
    [COMServerAssociation(AssociationType.AllFiles)]
    public class MazchuPicchu : SharpContextMenu
    {
        protected override bool CanShowMenu()
        {
            return true;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            var upperMenu = new ContextMenuStrip();
            var itemUpperMenu = new ToolStripMenuItem
            {
                Text = "파일명을 맞추픽추!",
                Image = Properties.Resources.Asuka
            };

            foreach (var i in SelectedItemPaths)
            {
                string[] splitVal = i.Split('\\');
                string filename = splitVal[splitVal.Length - 1];

                //filename(without ext) of each selected files
                string nameWithoutExt = filename.Substring(0, filename.LastIndexOf('.'));

                var newDropdownItem = new ToolStripMenuItem
                {
                    Text = nameWithoutExt
                };

                //When click, call the function
                string fullPathWithoutExt = i.Substring(0, i.LastIndexOf('.'));
                newDropdownItem.Click += (sender, args) => RenameAll(i);

                //Add each item to sub menu
                itemUpperMenu.DropDownItems.Add(newDropdownItem);

            }

            upperMenu.Items.Add(itemUpperMenu);

            return upperMenu;
        }

        //targetName : fullpath of target without extension
        //targetExt : extension of target file
        private void RenameAll(string fullPathTarget)
        {
            //한번 바꾼 적이 있는 확장자는 저장해두었다가 그다음부터는 인덱스를 매겨서 파일명을 바꿔주어야 한다.

            string targetWithoutExt = fullPathTarget.Substring(0, fullPathTarget.LastIndexOf('.'));
            string targetExt = fullPathTarget.Substring(fullPathTarget.LastIndexOf('.'));
			

            Dictionary<string, int> extCounts = new Dictionary<string, int>();


			//귀찮은 일이 생기지 않도록 일단 싹 바꿔둔다.(순서는 기억)
			int i = 0;
			foreach (var eachFullPath in SelectedItemPaths)
			{
				System.IO.File.Move(eachFullPath,fullPathTarget + ".MatchuPicchuTmp." + i++);
			}
			//실제 바꾸는 부분
			i = 0;
            foreach(var eachFullPath in SelectedItemPaths)
            {
                //파일명을 바꾼다.(바꾼 확장자에 대해서 인덱스를 1 늘려줌)
                string extension = eachFullPath.Substring(eachFullPath.LastIndexOf('.'));
				string extOut = extension;
				//처음 보는 확장자라면 카운트를 초기화해준다.
                if(extCounts.ContainsKey(extension) == false)
                    extCounts.Add(extension, 0);
				else//처음 보는 확장자가 아니라면 뒤에 (n)을 붙여줌
					extOut = "(" + extCounts[extension] + ")" + extension;

				

				++extCounts[extension];
				//파일명을 바꾼다
				System.IO.File.Move(fullPathTarget + ".MatchuPicchuTmp." + i++, targetWithoutExt + extOut);
            }
            return;
        }
    }
}