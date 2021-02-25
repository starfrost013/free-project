using Emerald.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Free
{
    [XmlRoot("Objects")]
    public class RootObjectCollection : IEnumerable
    {
        public List<RootObject> RootObjList { get; set; }

        public RootObjectCollection(List<RootObject> ObjList)
        {
            RootObjList = ObjList; // this should be okay
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // create a new instance of the rootobject enumerator and return
            return GetEnumerator();
        }

        public RootObjectCollectionEnumerator GetEnumerator()
        {
            return new RootObjectCollectionEnumerator(RootObjList);
        }
    }
    
    public class RootObjectCollectionEnumerator : IEnumerator
    {

        object IEnumerator.Current
        {
            get
            {
                return (object)Current;
            }
        }

        public RootObject Current { 
            get
            {
                try
                {
                    return RootObjList[Position];
                }
                catch (IndexOutOfRangeException err)
                {
                    Error.Throw(err, ErrorSeverity.FatalError, "An error occurred while operating on a RootObjectCollection: Error - attempted to acquire invalid RootObject!", "SDLEmerald", 501);
                    return null; // will never run
                }
            }

        }
        public int Position = -1;
        public List<RootObject> RootObjList { get; set; }

        public void Reset() => Position = -1;
        public bool MoveNext()
        {
            Position++;
            return (Position < RootObjList.Count);
        }

        public RootObjectCollectionEnumerator(List<RootObject> RootObjectList)
        {
            RootObjList = RootObjectList;
        }
    }
}
