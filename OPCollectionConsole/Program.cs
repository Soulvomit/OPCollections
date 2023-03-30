using BST;
using FrequencyAnalysis;
using SimpleCollections;
using System.Diagnostics;

namespace OPCollectionConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestFrequencyAnalysis();

            //TestBST();
            //TestQueue();
        }

        private static void TestFrequencyAnalysis()
        {
            //because all files use the same encryption key, we add them together to get a larger sample set 
            string encryptedText = File.ReadAllText("Crypt1.txt").ToLower() +
                 File.ReadAllText("Crypt2.txt").ToLower() +
                 File.ReadAllText("Crypt3.txt").ToLower();

            //analyse frequency of each letter in encrypted text 
            List<KeyValuePair<char, int>> list =
                FrequencyAnalyser.AnalyzeFrequency(encryptedText);
            //show results of frequency analysis
            Console.WriteLine("Frequency Analysis Results:");
            foreach (KeyValuePair<char, int> pair in list)
            {
                Console.WriteLine(pair.ToString());
            }

            //assume 'e' is the most common letter and calculate key
            int key = list[0].Key - 'e';
            //decrypt text using key
            string decryptedText = FrequencyAnalyser.CaesarDecrypt(encryptedText, key);
            //show result of decryption
            Console.WriteLine("\nKEY:\n" + key + " = " + list[0].Key + "-e" + "\nTEXT:\n" + decryptedText);

            //assume 'e' is the second most common letter and calculate key
            key = list[1].Key - 'e';
            //decrypt text using key
            decryptedText = FrequencyAnalyser.CaesarDecrypt(encryptedText, key);
            //show result of decryption
            Console.WriteLine("KEY:\n" + key + " = " + list[1].Key + "-e" + "\nTEXT:\n" + decryptedText);

            //assume 'e' is the third most common letter and calculate key
            key = list[2].Key - 'e';
            //decrypt text using key
            decryptedText = FrequencyAnalyser.CaesarDecrypt(encryptedText, key);
            //show result of decryption
            Console.WriteLine("KEY:\n" + key + " = " + list[2].Key + "-e" + "\nTEXT:\n" + decryptedText);

            //assume 't' is the most common letter and calculate key
            key = list[0].Key - 't';
            //decrypt text using key
            decryptedText = FrequencyAnalyser.CaesarDecrypt(encryptedText, key);
            //show result of decryption
            Console.WriteLine("KEY:\n" + key + " = " + list[0].Key + "-t" + "\nTEXT:\n" + decryptedText);
        }

        #region TestBST
        private static void TestBST()
        {
            OPBinarySearchTree<int> bst = new(10000 / 2);

            Random random = new Random();
            for (int i = 0; i < 9999; i++)
            {
                bst.Insert(random.Next(0, 10000));
            }

            bool result;
            result = bst.Find(100);

            List<int> list = new List<int>();
            bst.InOrder(bst.Root, ref list);

            OPBSTNode<int> min = bst.Min(bst.Root);
            OPBSTNode<int> max = bst.Max(bst.Root);

            OPBSTNode<int> node = bst.FindNode(100);
            OPBSTNode<int> min1 = bst.Min(node);
            OPBSTNode<int> max1 = bst.Max(node);
        }
        #endregion

        #region TestQueue
        private static void TestQueue()
        {
            Random random = new Random();
            OPQueue<int> q = new OPQueue<int>();
            for (int i = 0; i < 10000; i++)
            {
                //q.Enqueue(i);
                q.Enqueue(random.Next(0, 10000));
            }
            //foreach(int i in q)
            //{
            //    Console.WriteLine(i);
            //}
            Console.WriteLine("Count: " + q.Count);
            Thread t = new Thread(q.Sort, int.MaxValue);
            Stopwatch s = new Stopwatch();
            s.Start();
            t.Start();
            t.Join();
            //q.BubbleSortOptimized();
            s.Stop();
            Console.WriteLine("Time: " + s.ElapsedMilliseconds);
            //foreach (int i in q)
            //{
            //    Console.WriteLine(q.Dequeue());
            //}
            Console.WriteLine("Count: " + q.Count);
            Console.WriteLine("Rear Data: " + q.Rear.Data);
            Console.Read();
        }
        #endregion
    }
}