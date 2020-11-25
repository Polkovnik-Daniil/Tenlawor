using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            object obj = null;
            
            Services<int> que = new Services<int>();
            que.Add(obj, 1);
            que.Add(obj, 2);
            que.Add(obj, 3);
            que.Add(obj, 4);
            que.Add(obj, 5);
            que.Add(obj, 6);
            que.Add(obj, 7);
            que.Add(obj, 8);
            que.Add(obj, 9);

            Console.WriteLine("Value in queue:");
            que.print();
            Console.WriteLine("Fetch value");
            que.Remove(obj);
            Console.WriteLine("Value in queue:");
            que.print();
            Console.Write("Enter value(integer): ");
            int i = int.Parse(Console.ReadLine());
            bool bl = que.find(i);
            if (bl == true)
            {
                Console.WriteLine("Value was found");
            }
            else
            {
                Console.WriteLine("Value wasn't found");
            }
            Console.Write("Enter position from what you will delete value: ");
            int PosX = int.Parse(Console.ReadLine());
            Console.Write("Enter number: ");
            int kl = int.Parse(Console.ReadLine());
            que.del_elem(PosX, kl);
            que.print();
            Services<int> que_ = new Services<int>();
            que_.Copy_(que.queue);
            que_.print();

            ObservableCollection<Services<string>> users = new ObservableCollection<Services<string>>()         
            {
                new Services<string> { Size = "Value 1"},
                new Services<string> { Size = "Value 2"},
                new Services<string> { Size = "Value 3"}
            };
          
            users.CollectionChanged += Users_CollectionChanged;
            users.Add(new Services<string> { Size = "Value 4" });
            users.RemoveAt(1);
            users[0] = new Services<string> { Size = "Value 5" };

            foreach (Services<string> user in users)
            {
                Console.WriteLine(user.Size);
            }
        }
        public static void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    Services<string> newUser = e.NewItems[0] as Services<string>;
                    Console.WriteLine($"Добавлен новый объект: {newUser.Size}");
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    Services<string> oldUser = e.OldItems[0] as Services<string>;
                    Console.WriteLine($"Удален объект: {oldUser.Size}");
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                    Services<string> replacedUser = e.OldItems[0] as Services<string>;
                    Services<string> replacingUser = e.NewItems[0] as Services<string>;
                    Console.WriteLine($"Объект {replacedUser.Size} заменен объектом {replacingUser.Size}");
                    break;
            }
        }
    }
    class Services<T> : IOrderedDictionary
    {
        public Queue<T> queue;
        public bool bl;
        public string Size { get; set; }
        public Services()
        {
            queue = new Queue<T>();
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
        public object this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public bool IsFixedSize => throw new NotImplementedException();
        public bool IsReadOnly => throw new NotImplementedException();
        public ICollection Keys => throw new NotImplementedException();
        public ICollection Values => throw new NotImplementedException();
        public bool IsSynchronized => throw new NotImplementedException();
        public object SyncRoot => throw new NotImplementedException();
        public bool Contains(object key)
        {
            throw new NotImplementedException();
        }
        public IDictionaryEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
        
        public void Copy_(Queue<T> _1)
        {
            Clear_(queue);
            int k = _1.Count;
            List<T> list = new List<T>();
            for(int i = 0; i < k; i++)
            {
                list.Add(_1.Dequeue());
            }
            for(int i = 0; i < k; i++)
            {
                queue.Enqueue((T)list[i]);
                _1.Enqueue((T)list[i]);
            }
        }

        public void Clear_(Queue<T> _1)
        {
            for(int i = 0; i < _1.Count; i++)
            {
                _1.Dequeue();
            }
        }

        public void print()
        {
            foreach (var p in queue)
            {
                Console.WriteLine(p);
            }
        }

        public void del_elem(int PositionX , int kol)
        {
            int size = queue.Count, j = 0, i = 0, size_ = 0;
            object obj;
            if (size > PositionX && PositionX > 0 && PositionX + kol <= size)
            {
                List<T> list = new List<T>();
                for (i = 0; i < size; i++)
                {
                    if (i == PositionX && kol > 0)
                    {
                        PositionX++;
                        size_++;
                        kol--;
                        obj = queue.Dequeue();
                    }
                    else
                    {
                        obj = queue.Dequeue();
                        list.Add((T)obj);
                    }
                }
                for (i = 0; i < size - size_; i++)
                {
                    T l = list[i];
                    queue.Enqueue(l);
                }
            }
            else
            {
                throw new Exception("Value entered incorrectly");
            }
        }

        public bool find(T value)
        {
            bool bl = false;
            foreach (var p in queue)
            {
                if (Convert.ToInt32(p) == Convert.ToInt32(value))
                {
                    bl = true;
                }                
            }
            if (bl == true)
                return true;
            return false;
        }

        public void RemoveAt(int index)         //позиция
        {
            int size = queue.Count, j = 0, i = 0;
            if (size > index && index > 0)
            {
                List<T> list = new List<T>();
                for(i = 0, j = 0; i < queue.Count; i++, j++)
                {
                    if (i == index)
                    {
                        j--;
                    }
                    else
                    {
                        list[j] = queue.Dequeue();
                    }
                }
                for(i = 0; i < queue.Count - 1; i++)
                {
                    queue.Enqueue(list[i]);
                }
            }
            else
            {
                throw new Exception("Value entered incorrectly");
            }
        }

        public void Remove(object key)
        {
            try 
            {
                queue.Dequeue();
            }
            catch
            {
                throw new Exception("Fetch error");
            }
        }

        public void Add(object key, object value)
        {
            queue.Enqueue((T)value);
        }

        public void Insert(int index, object key, object value)
        {
            int size = queue.Count, j = 0, i = 0;
            if (size > index && index > 0)
            {
                List<T> list = new List<T>();
                for (i = 0, j = 0; i < queue.Count; i++, j++)
                {
                    if (i == index)
                    {
                        list[j] = (T)value;
                    }
                    else
                    {
                        list[j] = queue.Dequeue();
                    }
                }
                for (i = 0; i < queue.Count + 1; i++)
                {
                    queue.Enqueue(list[i]);
                }
            }
            else
            {
                throw new Exception("Value entered incorrectly");
            }
            queue.Enqueue((T)value);
        }

        public int Count => queue.Count;
    }
}