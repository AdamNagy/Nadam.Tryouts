 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastructures;

namespace Datastructures
{
    public class ChainedList
    {
        //<constructors>
        public ChainedList(int _size)
        {
            backboneVector = new int[_size+1];
            mut = new int[_size+1];
            fej = -1;
            szfej = 0;
            for(int i = 0; i < _size-1; ++i)
            {
                mut[i] = i+1;
            }
            mut[_size-1] = 0;
        }
        //</constructors>
        /***********************************************************************/
        //<class_variables>
        private int fej { get; set; }
        private int szfej { get; set; }

        private int[] backboneVector;
        private int[] mut;
        //</class_variables>
        /***********************************************************************/
        //<class_methods>
        public void delete(int _elem)
        {
            int k;
            int previous = 0;
            bool searchResoult = false;
            search(_elem, ref searchResoult, ref previous);

            if( searchResoult )
            {
                if(previous == 0)
                {
                    k = szfej;
                    fej = mut[k];
                }
                else
                {
                    k = mut[previous];
                    mut[previous] = mut[k];
                }
                mut[k] = szfej;
                szfej = k;
            }
        }

        public void search(int searchedElem, ref bool succes, ref int previous)
        {
            if (fej != -1)
            {
                int i = fej;
                while (i > -1 && i != 0 && searchedElem != backboneVector[i])
                {
                    previous = i;
                    i = mut[i];
                }

                if (i == 0)
                {
                    previous = 0;
                    succes = false;
                }
                else
                {
                    bool ret = (searchedElem == backboneVector[i]);
                    succes = (searchedElem == backboneVector[i]);
                }
            }
            else
            {
                previous = -1;
            }
        }

        public void insert(int _tpInsert)
        {
            if( szfej != -1 )
            {
                bool resoult = false; 
                int previous = 0, k;
                search(_tpInsert, ref resoult, ref previous);
                
                if( !resoult )
                {
                    k = szfej;
                    szfej = mut[k];
                    if( previous == 0 )
                    {
                        mut[k] = fej;
                        fej = k;
                    }
                    else
                    {
                        mut[k] = mut[previous];
                        mut[previous] = k;
                    }
                    backboneVector[k] = _tpInsert;
                }
            }
        }

        public int getElem(int _idx)
        {
            if (fej >= (_idx + 1)) { return backboneVector[mut[_idx]]; }
            else { throw new IndexOutOfRangeException("Requested index is out of list: " + _idx); }                
        }
        //</class_methods>
    }
}
