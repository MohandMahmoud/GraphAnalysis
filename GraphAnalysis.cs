using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    public static class GraphAnalysis
    {
        
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// <param name="vertices"></param>
        /// <param name="edges"></param>
        /// <param name="startVertex"></param>
        /// <param name="outputs"></param>

        /// <summary>
        /// Analyze the edges of the given DIRECTED graph by applying DFS starting from the given "startVertex" and count the occurrence of each type of edges
        /// NOTE: during search, break ties (if any) by selecting the vertices in ASCENDING alpha-numeric order
        /// </summary>
        /// <param name="vertices">array of vertices in the graph</param>
        /// <param name="edges">array of edges in the graph</param>
        /// <param name="startVertex">name of the start vertex to begin from it</param>
        /// <returns>return array of 3 numbers: outputs[0] number of backward edges, outputs[1] number of forward edges, outputs[2] number of cross edges</returns>
        public static int[] AnalyzeEdges(string[] vertices, KeyValuePair<string, string>[] edges, string startVertex)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            // Create adjacency list
            Dictionary<string, List<string>> adjacencyList = new Dictionary<string, List<string>>();
            foreach (string vertex in vertices)
            {
                adjacencyList[vertex] = new List<string>();
            }
            foreach (KeyValuePair<string, string> edge in edges)
            {
                adjacencyList[edge.Key].Add(edge.Value);
            }
            foreach (string vertex in vertices)
            {
                adjacencyList[vertex].Sort();
            }

            // Run DFS
            HashSet<string> visited = new HashSet<string>();
            int[] counts = new int[3];
            DFS(adjacencyList, startVertex, visited, counts);

            return counts;
        }

        private static void DFS(Dictionary<string, List<string>> adjacencyList, string vertex, HashSet<string> visited, int[] counts)
        {
            visited.Add(vertex);
            foreach (string neighbor in adjacencyList[vertex])
            {
                if (!visited.Contains(neighbor))
                {
                    // Edge is a tree edge
                    DFS(adjacencyList, neighbor, visited, counts);
                }
                else
                {
                    // Edge is a back edge, forward edge, or cross edge
                    int edgeType = GetEdgeType(adjacencyList, vertex, neighbor);
                    counts[edgeType]++;
                }
            }
        }

        private static int GetEdgeType(Dictionary<string, List<string>> adjacencyList, string source, string destination)
        {
            List<string> neighbors = adjacencyList[source];
            int destIndex = neighbors.IndexOf(destination);
            int sourceIndex = neighbors.IndexOf(source);

            if (destIndex == sourceIndex + 1)
            {
                return 1; // Forward edge
            }
            else if (destIndex > sourceIndex)
            {
                return 2; // Cross edge
            }
            else
            {
                return 0; // Back edge
            }
        }
        #endregion
    }


}
 

