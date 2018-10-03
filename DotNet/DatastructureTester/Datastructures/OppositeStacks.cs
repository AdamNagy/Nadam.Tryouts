using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastructures;

namespace Datastructures
{
    unsafe
    public class OppositeStacks : Stack
    {                   
        //<constructors>        
        public OppositeStacks(int _size)
        {
            
            int* p;

            

            //backBoneVector = new List<int>(_size);
            backBone = new int[_size];
            for (int i = 0; i < _size; ++i )
            {
                backBone[i] = 0;
            }
                topPtr[0] = -1;
            topPtr[1] = _size;
            MAX = _size;
        }
        //</constructors>
        /***********************************************************************/
        //<class_variables>
        new private int[] topPtr = new int[2];

        int[] backBone;

        //protected Exception ex { get; set; }
        //</class_variables>
        /***********************************************************************/
        //<class_methods>
        override public bool isFull()
        {
            return (topPtr[1] - topPtr[0] == 1);
        }

        public virtual bool isEmpty(int i)
        {
            ex = new Exception("Parameter should be either 0 or 1");
            switch (i)
            {
                case 0: return (topPtr[0] == -1);
                case 1: return (topPtr[1] == MAX);
                default: throw ex;
            }
        }

        public virtual void push(int i, int _toPush)
        {
            int x;
            ex = new Exception("Stack is full!");
            if (!isFull())
            {
                switch (i)
                {
                    case 0: ++topPtr[0]; backBone[topPtr[0]] = _toPush;
                        x = backBone[topPtr[0]]; x = backBone.Count(); break;
                    case 1: --topPtr[1]; backBone[topPtr[1]] = _toPush; break;
                }
                //backBoneVector.Add(_toPush);
            }
            else
            {
                throw ex;
            }
        }

        public virtual int pop(int i)
        {
            ex = new Exception("Stack is empty!");
            int ret = 0;
            if (!isEmpty(i))
            {
                //int ret = backBoneVector[topPtr[i]];
                switch (i)
                {
                    case 0: ret = backBone[topPtr[0]]; --topPtr[0]; break;
                    case 1: ret = backBone[topPtr[1]]; ++topPtr[1]; break;
                }
                return ret;
            }
            throw ex;
        }

        public virtual int top(int i)
        {
            ex = new Exception("Stack is empty!");
            if (!isEmpty(i))
            {
                //return backBoneVector[topPtr[i]];
                return backBone[topPtr[i]];
            }
            throw ex;
        }
        //</class_methods>
    }
}
