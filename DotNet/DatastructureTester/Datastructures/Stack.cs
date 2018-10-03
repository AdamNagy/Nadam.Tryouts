using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastructures;

namespace Datastructures
{
    public class Stack
    {
        //<constructors>
        public Stack()
        {
            backBoneVector = new List<int>();
            this.MAX = -1;
            this.topPtr = 0;
        }

        public Stack(int[] _init)
        {
            backBoneVector = new List<int>(_init);
            topPtr = _init.Length;
        }

        public Stack(int _initSize)
        {
            backBoneVector = new List<int>(_initSize);
            MAX = _initSize;
            topPtr = 0;
        }
        //</constructors>
        /***********************************************************************/
        //<class_variables>
        protected List<int> backBoneVector;
        protected int topPtr { get; set; }
        protected int MAX { get; set; }

        protected Exception ex {get; set; }
        //</class_variables>
        /***********************************************************************/
        //<class_methods>
        public virtual bool isFull()
        {
            return topPtr == MAX;
        }


        public virtual bool isEmpty()
        {
            return topPtr == 0;
        }

        public virtual void push(int _toPush)
        {
            ex = new Exception("Stack is full");
            if (!isFull())
            {
                backBoneVector.Add(_toPush);
                ++topPtr;
            }
            else
            {
                throw ex;
            }
        }

        public virtual int pop()
        {
            ex = new Exception("Stack is empty");
            if (!isEmpty())
            {
                --topPtr;
                int ret = backBoneVector[topPtr];
                return ret;
            }
            else
            {
                throw ex;
            }
        }

        public virtual int top()
        {
            ex = new Exception("Stack is full");
            if (!isEmpty())
            {
                return backBoneVector[topPtr];
            }
            else
            {
                throw ex;
            }
        }
        //</class_methods>
        /***********************************************************************/
    }
}
