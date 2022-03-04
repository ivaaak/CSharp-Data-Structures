namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IntegerTreeFactory
    {
        private Dictionary<int, IntegerTree> nodesByKey;

        public IntegerTreeFactory()
        {
            this.nodesByKey = new Dictionary<int, IntegerTree>();
        }

        public IntegerTree CreateTreeFromStrings(string[] input)
        {
            foreach (var inputLine in input)
            {
                var keys = inputLine.Split(' ').Select(int.Parse).ToArray();
                var parent = keys[0];
                var child = keys[1];

                this.AddEdge(parent, child);
            }
            return this.GetRoot();
        }

        public IntegerTree CreateNodeByKey(int key)
        {
            if (!this.nodesByKey.ContainsKey(key))
            {
                this.nodesByKey.Add(key, new IntegerTree(key));
            }
            return this.nodesByKey[key];
        }

        public void AddEdge(int parent, int child)
        {
            var parentNode = new IntegerTree(parent);
            var childNode = new IntegerTree(child);

            childNode.AddParent(parentNode);
            parentNode.AddChild(childNode);
        }

        public IntegerTree GetRoot()
        {
            foreach (var kvp in this.nodesByKey)
            {
                if(kvp.Value.Parent == null)
                {
                    return kvp.Value;
                }
            }
            return null;
        }
    }
}
