
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace TestConsoleApp
{
    class Program
    {


        public static string FindIntersection(string[] strArr)
        {

            var result = strArr[0].Replace(" ", "").Split(',').Intersect(
             strArr[1].Replace(" ", "").Split(','));

            var value = string.Empty;
            foreach (var val in result)
            {
                value = $"{value}{val.Trim()},";
            }
            return value.TrimEnd(',') == string.Empty ? "false" : value.TrimEnd(',');
        }
        public static int[] BubbleSort(int[] input)
        {
            var temp = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                for (int j = 0; j < input.Length - i - 1; j++)
                    if (input[j] > input[j + 1])
                    {
                        temp = input[j];
                        input[j] = input[j + 1];
                        input[j + 1] = temp;
                    }
            }
            return input;
        }
        static long arrayManipulation(int n, int[][] queries)
        {
            var a = new int[n];

            for (int i = 0; i < queries[0].Length; i++)
            {
                for (int j = queries[i][0] - 1; j < queries[i][1]; j++)
                {
                    a[j] = a[j] + queries[i][2];
                }
                foreach (var item in a)
                {
                    Console.Write(item);
                }
            }
            return a.Max();

        }
        public static int[] LeftShift(int[] input, int d)
        {
            Queue<int> q = new Queue<int>(input);
            while (d > 0)
            {
                var e = q.Dequeue();
                //Console.WriteLine(e);
                //Console.WriteLine(q.Count());
                q.Enqueue(e);
                d--;
            }
            return q.ToArray();
        }
        public static string CubeRoot(int num)
        {
            var cubes_10 = "0:0,1:1,8:8,27:7,64:4,125:5,216:6,343:3,512:2,729:9".Split(',')
            .Select(x => new { Key = int.Parse(x.Split(':')[0]), Value = int.Parse(x.Split(':')[1]) });

            var arr = num.ToString().ToCharArray();
            var last = num.ToString().TakeLast(3).Select(x => int.Parse(x.ToString())).ToArray();
            var first = int.Parse(num.ToString().Substring(0, num.ToString().Length - 3));

            // answer will be stored here
            int lastDigit = 0, firstDigit = 0, index = 0;

            // get last digit of cube root
            foreach (var i in cubes_10)
            {
                if (index == last[last.Count() - 1]) { lastDigit = i.Value; }
                index++;
            }

            // get first digit of cube root
            index = 0;
            foreach (var i in cubes_10)
            {
                //Console.WriteLine(firstDigit);
                if (i.Key <= first) { firstDigit = index; }
                index++;
            }

            // return cube root answer
            return firstDigit.ToString() + lastDigit.ToString();

        }
        public static string AlphabetRunEncryption(string str)
        {
            var alphabets = "abcdefghijklmnopqrstuvwxyz";
            var alphabetsArr = alphabets.ToCharArray();
            var reverseAlphabets = new string(alphabets.Reverse().ToArray());
            var strArr = str.ToCharArray();
            var result = string.Empty;
            int lastIndex = alphabets.IndexOf(strArr[0]);
            int currentIndex = 0;
            var startChar = strArr[0] == 'a' ? 'a' : alphabetsArr[alphabets.IndexOf(strArr[0]) - 1];
            bool leftToRight = true;
            var searchAlphabets = alphabets;
            for (int i = 1; i < str.Length; i++)
            {
                currentIndex = alphabets.IndexOf(strArr[i]);
                lastIndex = alphabets.IndexOf(strArr[i - 1]);
                leftToRight = lastIndex < currentIndex;

                if (leftToRight)
                {
                    searchAlphabets = alphabets;
                    currentIndex = searchAlphabets.IndexOf(strArr[i]);
                    lastIndex = searchAlphabets.IndexOf(strArr[i - 1]);
                }
                else
                {
                    searchAlphabets = reverseAlphabets;
                    currentIndex = searchAlphabets.IndexOf(strArr[i]);
                    lastIndex = searchAlphabets.IndexOf(strArr[i - 1]);
                }
                if (currentIndex - lastIndex == 1)
                {
                    startChar = leftToRight ? (strArr[0] == 'a' ? 'a' : alphabetsArr[alphabets.IndexOf(strArr[i - 1]) - 1])
                    : (strArr[0] == 'z' ? 'z' : alphabetsArr[alphabets.IndexOf(strArr[i - 1])]);
                }

                if (currentIndex < 0)
                {
                    switch (strArr[i])
                    {
                        case 'L': break;
                        case 'R': break;
                        case 'N':
                            result += $"{startChar}{strArr[i - 1]}{strArr[i - 1]}";
                            break;
                        case 'S':
                            result += $"{startChar}{strArr[i - 1]}";
                            break;
                    }
                }
                else
                {

                    alphabetsArr = searchAlphabets.ToCharArray();
                    var diff = lastIndex - currentIndex;
                    if (leftToRight)
                    {
                        if (diff != -1)
                        {
                            if ("LRNS".IndexOf(strArr[i]) > -1)
                            {
                                result += $"{startChar}{strArr[i - 1]}";
                                startChar = alphabetsArr[searchAlphabets.IndexOf(strArr[i - 1]) - 1]; ;
                            }
                            else
                            {
                                result += $"{startChar}{strArr[i]}";
                                startChar = alphabetsArr[searchAlphabets.IndexOf(strArr[i]) - 1]; ;
                            }
                        }
                    }



                    Console.WriteLine($"{leftToRight}-{diff}-{startChar}-{strArr[i]}-{lastIndex}-{currentIndex}-[{result}]");
                }
            }

            // code goes here  
            return result;

        }
        static void Main()
        {
            // keep this function call here
            //Console.WriteLine(FindIntersection(Console.ReadLine().Split(':')));
            WebRequest request = WebRequest.Create("https://coderbyte.com/api/challenges/json/age-counting");
            //WebResponse response = request.GetResponse();

            var input = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    input = reader.ReadToEnd();
                }
            }
            var result = 0;
            var value = input.TrimEnd('}').Split(":")[1].Split(",").Select(a => new { key = a.Split('=')[0], value = a.Split('=')[1] });
            foreach (var item in value)
            {
                if (item.key.ToLower() == "age")
                {
                    if (int.TryParse(item.value, out int age) && age > 0)
                    {
                        result++;
                    }

                }
            }
            //var str="code goes here"; 
            //Console.WriteLine(CubeRoot(148877));
            //Console.WriteLine(AlphabetRunEncryption("abS"));
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 8, 10 };
            Stack<int> stack = new Stack<int>(arr.AsEnumerable());
            stack.Push(12);
            //Console.WriteLine(stack.Pop());
            // foreach (var item in BubbleSort(new int[] { 8, 1, 4, 2, 5, 3, 6 }))
            {
                //    Console.WriteLine(item);
            }

            foreach (var item in LeftShift(new int[] { 1, 2, 3, 4, 5 }, 4))
            {
                Console.WriteLine(item);
            }

        }

    }
}