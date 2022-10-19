using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;

namespace Trees
{
    public class Node
    {
        private readonly string _value;
        private readonly List<Node> _children = new List<Node>();

        public Node(string value)
        {
            _value = value;
        }


        public Node Parent { get; private set; }

        public string Value { get { return _value; } }

        public ReadOnlyCollection<Node> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public Node AddChild(string value)
        {
            var node = new Node(value) { Parent = this };
            _children.Add(node);
            return node;
        }

        public bool RemoveChild(Node node)
        {
            return _children.Remove(node);
        }

        public static List<string> Traverse(Node root)
        {
            var list = new List<string>();
            foreach (var child in root.Children)
            {
                GetTraversal(child, "", list);
            }
            for(var i = 0; i< list.Count; i++)
            {
                list[i] = $"{root.Value}/{list[i]}";
            }

            return list;
        }

        private static void GetTraversal(Node node, string path, List<string> list)
        {
            path = path == "" ? node.Value : path + "/" + node.Value;
            if (node.Children.Count == 0)
            {
                list.Add(path);
            }
            else
            {
                foreach (var child in node.Children)
                {
                    GetTraversal(child, path, list);
                }
            }
        }

        public static void Main(string[] args)
        {
            Node n = new("localhost:8080");

            var firstChild = n.AddChild("users");
            var second = n.AddChild("orders");
            var third = n.AddChild("invoices");

            var f1 = firstChild.AddChild("user1");
            var f2 = firstChild.AddChild("user2");

            var s1 = second.AddChild("order1");
            var s2 = second.AddChild("order2");

            var t1 = third.AddChild("invoices1");
            var t2 = third.AddChild("invoices2");


            var list = Traverse(n);

            StreamWriter sw = new StreamWriter("C:\\Users\\User\\Desktop\\PageLinks.txt");

            foreach (var s in list)
                sw.WriteLine(s);

            sw.Close();
        }
     }

        
 }
