namespace Problem01.CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CircularQueue<T> : IAbstractQueue<T>
    {
        private T[] elements;
        private int startIndex, endIndex;

        public CircularQueue(int capacity)
        {
            this.elements = new T[capacity];
        }

        public int Count { get; set; }

        public void Enqueue(T item)
        {
            if(this.Count >= this.elements.Length)
            {
                this.Grow();
            }
            this.elements[this.endIndex] = item;
            this.endIndex = (this.endIndex + 1) % this.elements.Length;
            this.Count++;
        }

        public T[] ToArray()
        {
            return this.CopyElements(new T[this.Count]);
        }

        public T Dequeue()
        {
            if(this.elements.Length == 0)
            {
                throw new InvalidOperationException();
            }

            var head = this.elements[startIndex];

            if(this.Count == 1)
            {
                this.elements[startIndex] = this.elements[endIndex] = default;
                this.Count = 0;

                return head;
            }
            if(startIndex == this.elements.Length - 1)
            {
                startIndex = 0;
            }

            startIndex = (startIndex + 1) % this.elements.Length;
            this.Count--;
            return head;
        }
        
        public T Peek()
        {
            if(this.elements.Length == 0)
            {
                throw new InvalidOperationException();
            }

            return elements[this.Count-1];
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for (int currentIndex = 0; currentIndex < this.Count; currentIndex++)
            {
                var index = (this.startIndex + currentIndex) % this.elements.Length;
                yield return this.elements[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void Grow()
        {
            this.elements = this.CopyElements(new T[this.elements.Length * 2]);
            this.startIndex = 0;
            this.endIndex = this.Count;
        }

        private T[] CopyElements(T[] resultArr)
        {
            for (int i = 0; i < this.Count; i++)
            {
                resultArr[i] = this.elements[(this.startIndex + i) % this.elements.Length];
            }

            return resultArr;
        }
    }
}
