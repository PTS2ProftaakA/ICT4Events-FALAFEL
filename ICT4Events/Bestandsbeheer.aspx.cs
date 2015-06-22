using System;
using System.IO;
using System.Web.UI.WebControls;

namespace ICT4Events
{
    public partial class Bestandsbeheer : System.Web.UI.Page
    {
        private string _currentDir, _currentFile;

        protected void Page_Load(object sender, EventArgs e)
        {
            dlSubmit.ServerClick += dlSubmit_ServerClick;
            fileSubmit.ServerClick += fileSubmit_ServerClick;
            dirSubmit.ServerClick += dirSubmit_ServerClick;

            if (!IsPostBack) RefreshFileTree();

            string valuePath = fileTree.SelectedNode.ValuePath;
            if (Path.GetExtension(valuePath) != String.Empty)
            {
                _currentDir = Path.GetDirectoryName(Server.MapPath(valuePath));
                _currentFile = Server.MapPath(valuePath);
            }
            else
            {
                _currentDir = Server.MapPath(valuePath);
                _currentFile = String.Empty;
            }
        }

        private void RefreshFileTree()
        {
            //TODO Root MapPath aanpassen naar definitieve locatie
            var info = new DirectoryInfo(Server.MapPath("/TEST/"));
            fileTree.Nodes.Clear();
            fileTree.Nodes.Add(BuildTreeNode(info, null));
            fileTree.Nodes[0].Select();
            fileTree.Nodes[0].Expand();
        }

        private TreeNode BuildTreeNode(DirectoryInfo dirInfo, TreeNode parentNode)
        {
            if (dirInfo == null) return null;

            if (dirInfo.Exists)
            {
                TreeNode node = new TreeNode(dirInfo.Name)
                {
                    SelectAction = TreeNodeSelectAction.SelectExpand,
                    ImageUrl = @"http://thijsiez.nl/folder.png"
                };

                DirectoryInfo[] subDirs = dirInfo.GetDirectories();
                for (int i = 0; i < subDirs.Length; i++)
                {
                    BuildTreeNode(subDirs[i], node);
                }

                FileInfo[] files = dirInfo.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    node.ChildNodes.Add(new TreeNode(files[i].Name)
                    {
                        SelectAction = TreeNodeSelectAction.Select
                    });
                }

                if (parentNode != null)
                {
                    parentNode.ChildNodes.Add(node);
                    return parentNode;
                }
                return node;
            }
            return null;
        }

        private void fileSubmit_ServerClick(object sender, EventArgs e)
        {
            if (fileInput.PostedFile != null && fileInput.PostedFile.ContentLength > 0)
            {
                string targetFile = Path.Combine(_currentDir,
                    Path.GetFileName(fileInput.PostedFile.FileName));
                try
                {
                    if (!File.Exists(targetFile))
                    {
                        fileInput.PostedFile.SaveAs(targetFile);
                        RefreshFileTree();
                    }
                    else
                    {
                        //TODO ERROR: Bestand bestaat al
                    }
                }
                catch (System.Web.HttpException)
                {
                    //TODO ERROR: Uploaden mislukt
                }
            }
        }

        private void dirSubmit_ServerClick(object sender, EventArgs e)
        {
            string newDirPath = Path.Combine(_currentDir, dirInput.Value);
            if (!Directory.Exists(newDirPath))
            {
                Directory.CreateDirectory(newDirPath);
                dirInput.Value = String.Empty;
                RefreshFileTree();
            }
            else
            {
                //TODO ERROR: Map bestaat al
            }
        }

        private void dlSubmit_ServerClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_currentFile))
            {
                Response.AddHeader("content-disposition", "attachment; filename=" + Path.GetFileName(_currentFile));
                Response.WriteFile(_currentFile);
                Response.End();
            }
            else
            {
                //TODO ERROR: Geen bestand geselecteerd
            }
        }
    }
}