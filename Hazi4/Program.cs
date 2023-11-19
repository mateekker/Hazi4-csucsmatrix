using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazi4
{
    class Program
    {
        class Graf
        {
            int[,] matrix;
            int N; // csúcsok száma
            int M; // élek száma

            public Graf()
            {
                string[] sortomb = Console.ReadLine().Split(' ');
                N = int.Parse(sortomb[0]);
                M = int.Parse(sortomb[1]);

                matrix = new int[N, N];

                for (int i = 0; i < M; i++)
                {
                    string[] temp = Console.ReadLine().Split(' ');
                    matrix[int.Parse(temp[0]), int.Parse(temp[1])] = 1;
                    matrix[int.Parse(temp[1]), int.Parse(temp[0])] = 1;
                }
            }

            public bool Van_el(int a, int b)
            {
                return matrix[a, b] == 1;
            }

            public int Fokszam(int a)
            {
                int result = 0;
                for (int i = 0; i < N; i++)
                {
                    if (matrix[a, i] == 1) result++;
                }
                return result;
            }

            public bool Izolalt(int a)
            {
                return Szomszedai(a).Count == 0;
            }

            public bool Van_hurok(int a)
            {
                return Van_el(a, a);
            }

            public bool Elerheto(int honnan, int hova)
            {
                int feher = 0;
                int kek = 1;
                int piros = 2;

                int[] szin = new int[N];

                Queue<int> tennivalok = new Queue<int>();
                tennivalok.Enqueue(honnan);
                szin[honnan] = kek;

                while (tennivalok.Count != 0)
                {
                    int tennivalo = tennivalok.Dequeue();

                    if (tennivalo == hova)
                        return true;

                    szin[tennivalo] = piros;

                    foreach (int szomszed in Szomszedai(tennivalo))
                    {
                        if (szin[szomszed] == feher)
                            tennivalok.Enqueue(szomszed);

                        szin[szomszed] = kek;
                    }
                }

                return false;
            }

            List<int> Szomszedai(int a)
            {
                List<int> result = new List<int>();

                for (int i = 0; i < N; i++)
                {
                    if (matrix[a, i] == 1)
                    {
                        result.Add(i);
                    }
                }

                return result;
            }

            public int Komponens_szamossaga(int honnan)
            {
                int feher = 0;
                int kek = 1;
                int piros = 2;

                int[] szin = new int[N];

                Queue<int> tennivalok = new Queue<int>();
                tennivalok.Enqueue(honnan);
                szin[honnan] = kek;

                int result = 0;

                while (tennivalok.Count != 0)
                {
                    int tennivalo = tennivalok.Dequeue();

                    szin[tennivalo] = piros;
                    result += 1;

                    foreach (int szomszed in Szomszedai(tennivalo))
                    {
                        if (szin[szomszed] == feher)
                            tennivalok.Enqueue(szomszed);

                        szin[szomszed] = kek;
                    }
                }

                return result;
            }

            public List<int> Komponens(int a)
            {
                int feher = 0;
                int kek = 1;
                int piros = 2;

                int[] szin = new int[N];

                Queue<int> tennivalok = new Queue<int>();
                tennivalok.Enqueue(a);
                szin[a] = kek;

                List<int> result = new List<int> { };

                while (tennivalok.Count != 0)
                {
                    int tennivalo = tennivalok.Dequeue();

                    szin[tennivalo] = piros;
                    result.Add(tennivalo);

                    foreach (int szomszed in Szomszedai(tennivalo))
                    {
                        if (szin[szomszed] == feher)
                            tennivalok.Enqueue(szomszed);

                        szin[szomszed] = kek;
                    }
                }

                return result;
            }

            public int Komponensek_szama()
            {
                int result = 0;

                int feher = 0;
                int piros = 2;
                int[] szin = new int[N];

                for (int i = 0; i < N; i++)
                {
                    List<int> komponens = Komponens(i);
                    bool temp = true;
                    foreach (int n in komponens)
                    {
                        if (szin[n] == feher && temp)
                        {
                            result++;
                            temp = false;
                        }

                        szin[n] = piros;
                    }
                }

                return result;
            }

            public List<int> Legrovidebb_ut(int start, int end)
            {
                int feher = 0;
                int kek = 1;
                int piros = 2;

                int[] szin = new int[N];

                Queue<int> tennivalok = new Queue<int>();
                tennivalok.Enqueue(start);
                szin[start] = kek;

                int[] honnan = new int[N];
                for (int i = 0; i < honnan.Length; i++)
                {
                    honnan[i] = -2;
                }
                honnan[start] = -1;

                while (tennivalok.Count != 0)
                {
                    int tennivalo = tennivalok.Dequeue();

                    if (tennivalo == end)
                        return honnan_vektor_fel(honnan, end);

                    szin[tennivalo] = piros;

                    foreach (int szomszed in Szomszedai(tennivalo))
                    {
                        if (szin[szomszed] == feher)
                        {
                            tennivalok.Enqueue(szomszed);
                            szin[szomszed] = kek;
                            honnan[szomszed] = tennivalo;
                        }

                    }
                }

                return new List<int>();
            }

            private List<int> honnan_vektor_fel(int[] honnan, int end)
            {
                List<int> result = new List<int>();

                int node = end;

                while (node != -1)
                {
                    result.Add(node);
                    node = honnan[node];
                }

                result.Reverse();
                return result;
            }
        }
        static void Main(string[] args)
        {
        }
    }
}
