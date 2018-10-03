using System;
using System.Collections.Generic;

namespace Datastructures
{
    public class StackChain
    {
        //<constructors>
        public StackChain(int stacks, int k)
        {
            szfej = 0;
            backBone = new List<int>(k);
            mut = new List<int>(k);
            fej = new int[stacks];
            for( int i = 0; i < stacks; ++i )
            {
                fej[i] = 0;
            }

            for( int i = 0; i < k; ++i )
            {
                //mut[i] = i + 1;
                mut.Add(i + 1);
            }
        }
        //</constructors>
        /***********************************************************************/
        //<class_variables>
        uint k;
        int[] fej;//[30]; //vermek első elemeinek 'mutatója' (indexe)
        //unsigned int MUT[200]; //
        //std::vector<int> MUT;
        List<int> mut;
        int szfej; //első szabad hely indexe
        //std::vector<int> V; //backbone list
        List<int> backBone;
        protected Exception ex { get; set; }
        //</class_variables>
        /***********************************************************************/
        //<class_methods>
        public int top(int i)
        {
            ex = new Exception(i + ". stack is empty!");
            if (fej[i] != -1)
            {
                return backBone[fej[i]];
            }
            else
            {
                throw ex;
            }
        }

        public void push(int i, int toPush)
        {
            ex = new Exception(i + ". stack is full!");
            int newEmpty = newElem();
            if (newEmpty != -1)
            {
                mut[newEmpty] = fej[i];
                backBone.Add(0); //just to have a new place in the list
                backBone[newEmpty] = toPush;
                fej[i] = newEmpty;
            }
            else
            {
                throw ex;
            }
        }

        private void dispose(int szabad)
        {
            mut[szabad] = szfej;
            szfej = szabad;
        }
        public int pop(int i)
        {
            ex = new Exception(i + ". stack is empty!");
            if (fej[i] != -1)
            {
                int ret = backBone[fej[i]];
                int temp = mut[fej[i]];
                dispose(fej[i]);
                fej[i] = temp;

                return ret;
            }
            else
            {
                throw ex;
            }
        }
        private int newElem()
        {
            int ret = szfej;
            if (ret != -1)
            {
                mut.Add(0); //just to have a new place in the list
                szfej = mut[szfej];
                return ret;
            }
            return -1;
        }
        //</class_methods>
    }
}
