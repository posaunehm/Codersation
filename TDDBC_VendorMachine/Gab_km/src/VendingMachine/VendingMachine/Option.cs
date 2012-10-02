using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public abstract class Option<T>
    {
        protected bool isSome;
        public bool IsSome
        {
            get
            {
                return isSome;
            }
        }

        public bool IsNone
        {
            get
            {
                return !isSome;
            }
        }

        public abstract T Value { get; protected set; }

        public static Option<T> Some(T value)
        {
            return new Some<T>(value);
        }

        public static Option<T> None()
        {
            return new None<T>();
        }
    }

    public class Some<T> : Option<T>
    {
        private T _value;

        internal Some(T value)
        {
            this._value = value;
            this.isSome = true;
        }

        public override T Value
        {
            get
            {
                return this._value;
            }
            protected set
            {
                this._value = value;
            }
        }
    }

    public class None<T> : Option<T>
    {
        internal None()
        {
            this.isSome = false;
        }

        public override T Value
        {
            get
            {
                throw new NullReferenceException();
            }
            protected set
            {
                throw new NullReferenceException();
            }
        }
    }
}
