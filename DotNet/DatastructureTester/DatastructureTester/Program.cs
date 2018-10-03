using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastructures;

namespace DatastructureTester
{
    class Program
    {
        //<Stack_tester>
            //simple stack tests
        void stackTest1()
        {
            Stack stack = new Stack();

            for (int i = 0; i < 10; ++i)
            {
                stack.push(i * 2);
            }

            while( !stack.isEmpty() )
            {
                Console.Write(stack.pop() + " ");
            }
        }

        void stackTest2()
        {
            int[] elems = new int[] { 2, 4, 6, 8 };
            Stack stack = new Stack(elems);

            stack.push(10);
            while (!stack.isEmpty())
            {
                Console.Write(stack.pop() + " ");
            }
        }

            //opposit stacks test
        void oppositStacksTester()
        {
            int testSize = 150;
            OppositeStacks oppStack = new OppositeStacks(testSize);
            int i, x = 0;

            try
            {
                //for (i = 65; !oppStack.isFull(); ++i)
                for (char c = 'a'; c <= 'z'; ++c)
                {
                    ++x;
                    oppStack.push(0, (int)c);
                }

                //cout << "i : " << i << endl;
                Console.WriteLine("x: " + x);

                while (!oppStack.isEmpty(0))
                {
                    oppStack.push(1, oppStack.pop(0));
                }
                //cout << "\n\n";
                Console.WriteLine("\n\n");

                while (!oppStack.isEmpty(1))
                {
                    //cout << (char)oppStack.pop(1) << " ";
                    Console.Write(String.Format("{0} ", (char)oppStack.pop(1)));
                }
                Console.WriteLine("\n");
                for (char c = 'a'; c <= 'z'; ++c)
                {
                    oppStack.push(0, (int)c);
                }
                while (!oppStack.isEmpty(0))
                {
                    Console.Write(String.Format("{0} ", (char)oppStack.pop(0)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine(ex.ToString());
            }
        }

            //stack chain test
        void kStacksChainTest()
        {
            int elems = 10, stacks = 5;
            KStackChain nStacks = new KStackChain(stacks, elems * stacks + 1);

            for( int i = 0; i < elems; ++i )
            {
                nStacks.push(0, i+1);
                nStacks.push(1, elems - i);
                nStacks.push(2, (i+1)*(i+1));
                nStacks.push(3, 100 + i);
                nStacks.push(4, i*3);
            }

            for( int i = 0; i < elems; ++i )
            {
                for( int j = 0; j < stacks; ++j )
                {
                    Console.Write(String.Format("{0,3} ", nStacks.pop(j))); 
                }
                Console.WriteLine("\n");
            }
        }
        //</Stack_tester>

        //<ChainedList_tester>
        void chainedListTest()
        {
            ChainedList myList = new ChainedList(5);

            for( int i = 0; i < 5; ++i)
            {
                myList.insert(10+i);
            }

            try
            {
                for (int i = 0; i < 5; ++i)
                {
                    Console.WriteLine(myList.getElem(i));
                }
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //</ChainedList_tester>
        void tester()
        {
            List<int> t = new List<int>();
            try
            {
                t.Add(0);

                t[0] = 3;
                for( int j = 0; j < 10; ++j )
                {

                    Console.Write(t[0] + " ");
                }
            }
            catch(ArgumentOutOfRangeException ex)
            {
                Console.Write("\nArgumentOutOfRangeException");
            }                           
        }      

        static void Main(string[] args)
        {
            Program prog = new Program();
            
            //Console.WriteLine("Stack test 1: ");
            //prog.stackTest1();

            //Console.WriteLine("\nStack test 2: ");
            //prog.stackTest2();

            //Console.WriteLine("\nOpposit stacks test 1");
            //prog.oppositStacksTester();
            
            //Console.WriteLine("\nK stacks chain test");
            //prog.kStacksChainTest();
            
            //Console.WriteLine("Chained list tests");
            //prog.chainedListTest();

            //prog.binaryTreeTester();

            //prog.tester();

            //prog.treeTester();

            Console.WriteLine("\n\n**************************\nTests end");
            Console.ReadKey();
        }
    }
}
