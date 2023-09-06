using System.Collections.Generic;

namespace DICE.Modules.ViewModels.Cloud
{
    public class MegaTestDatasetViewModel
    {
        public class TreeNode<T>
        {
            public T Data { get; set; }
            public object AdditionalData { get; set; }
            public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();
        }

        public static TreeNode<string> MakeTree()
        {
            TreeNode<string> root = new TreeNode<string>() { Data = "All Files" };
            {
                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "Folder1" };
                    node.Children.Add(new TreeNode<string>() { Data = "Folder2" });
                    node.Children.Add(new TreeNode<string>() { Data = "Folder3" });
                    root.Children.Add(node);
                }

                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "Folder4" };
                    node.Children.Add(new TreeNode<string>() { Data = "Folder5" });
                    node.Children.Add(new TreeNode<string>() { Data = "Folder6" });
                    node.Children.Add(new TreeNode<string>() { Data = "Folder7" });
                    root.Children.Add(node);
                }

                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "Folder8" };
                    node.Children.Add(new TreeNode<string>() { Data = "Folder9" });
                    node.Children.Add(new TreeNode<string>() { Data = "Folder10" });
                    {
                        TreeNode<string> node2 = new TreeNode<string>() { Data = "Folder11" };
                        node2.Children.Add(new TreeNode<string>() { Data = "Folder12" });
                        node2.Children.Add(new TreeNode<string>() { Data = "Folder13" });
                        node.Children.Add(node2);
                    }
                    root.Children.Add(node);

                }
            }

            return root;
        }
    }
}
