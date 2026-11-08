namespace Backend;

    public class DoublyLinkedList<T> where T : IComparable<T> 
    {
    private DoubleNode<T>? head;      // node head
    private DoubleNode<T>? tail;         // node tail
    private int count;
        private bool ascending;

        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
            ascending = true;
        }

        public bool IsEmpty() => head == null;
        public int Count => count;

        private int Compare(T first, T second)
        {
            if (ascending)
            {
                return first.CompareTo(second);
            }
            return second.CompareTo(first);
        }

    // Addition: Insertion in automatic ascending/descending order
    public void Add(T data)
        {
            DoubleNode<T> newNode = new DoubleNode<T>(data);

        // Case 1: Empty list
        if (head == null)
            {
                head = newNode;
                tail = newNode;
                count++;
                return;
            }

            DoubleNode<T>? current = head;

        // Traverse as long as the current node exists and its data meets the ordering condition.
        while (current != null && Compare(current.Data, data) <= 0)
            {
                current = current.Next;
            }

            // Case 2: Insert at the beginning (if current is still head)
            if (current == head)
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }
            // Case 3: Insert at the end (if current is null, it means it's greater than all existing nodes)
            else if (current == null)
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            // Case 4: Insert in the middle (between current.Prev and current)
            else
            {
                DoubleNode<T> prev = current.Prev!;
                prev.Next = newNode;
                newNode.Prev = prev;
                newNode.Next = current;
                current.Prev = newNode;
            }

            count++;
        }

        // 3.2 Show forward
        public void DisplayForward()
        {
            if (IsEmpty())
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            DoubleNode<T>? current = head;
            Console.Write("Lista (Hacia adelante): ");
            while (current != null)
            {
                Console.Write(current.Data + (current.Next != null ? ", " : ""));
                current = current.Next;
            }
            Console.WriteLine();
        }

    // 3.3 Show backwards (usando Prev)
    public void DisplayBackward()
        {
            if (IsEmpty())
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            DoubleNode<T>? current = tail;
            Console.Write("Lista (Hacia atrás): ");
            while (current != null)
            {
                Console.Write(current.Data + (current.Prev != null ? ", " : ""));
                current = current.Prev;
            }
            Console.WriteLine();
        }

        // 3.4 Sort descending (inverts flag and physical pointers)
        public void SortDescending()
        {
            if (head == null || head == tail) return;

        //We change the sorting state
        ascending = !ascending;

            DoubleNode<T>? current = head;
            DoubleNode<T>? temp = null;

            while (current != null)
            {
                temp = current.Prev;
                current.Prev = current.Next;
                current.Next = temp;
                current = current.Prev; // We move forward along the inverted pointer
        }

            temp = head;
            head = tail;
            tail = temp;

            Console.WriteLine("La lista ha cambiado su orden a descendente.");
        }

    // 3.5 Show the mode(s)
    public void DisplayModes()
    {
        if (IsEmpty())
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }

        Dictionary<T, int> frequencies = new Dictionary<T, int>();
        DoubleNode<T>? current = head;
        int maxFrequency = 0;

        while (current != null)
        {
            if (frequencies.ContainsKey(current.Data))
                frequencies[current.Data]++;
            else
                frequencies[current.Data] = 1;

            if (frequencies[current.Data] > maxFrequency)
            {
                maxFrequency = frequencies[current.Data];
            }

            current = current.Next;
        }

        List<T> modes = new List<T>();
        foreach (var pair in frequencies)
        {
            if (pair.Value == maxFrequency)
            {
                modes.Add(pair.Key);
            }
        }

        if (maxFrequency == 1 && modes.Count == frequencies.Count)
        {
            Console.WriteLine("No hay una moda clara (todos aparecen la misma cantidad de veces).");
        }
        else if (modes.Count == 1)
        {
            Console.WriteLine($"La moda es {modes[0]}.");
        }
        else if (modes.Count == 2)
        {
            Console.WriteLine($"Las modas son {modes[0]} y {modes[1]}.");
        }
        else
        {
            T last = modes[modes.Count - 1];
            string rest = string.Join(", ", modes.GetRange(0, modes.Count - 1));
            Console.WriteLine($"Las modas son {rest} y {last}.");
        }
    }
    // 3.6 Show occurrence chart
    public void DisplayChart()
        {
            if (IsEmpty())
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            Dictionary<T, int> frequencies = new Dictionary<T, int>();
            DoubleNode<T>? current = head;

            while (current != null)
            {
                if (frequencies.ContainsKey(current.Data))
                    frequencies[current.Data]++;
                else
                    frequencies[current.Data] = 1;

                current = current.Next;
            }

            Console.WriteLine("\n--- Gráfico de Ocurrencias ---");
            foreach (var pair in frequencies)
            {
                string stars = new string('*', pair.Value);
                Console.WriteLine($"{pair.Key} {stars}");
            }
            Console.WriteLine("------------------------------");
        }

        // 3.7 Existe   
        public bool Exists(T value)
        {
            DoubleNode<T>? current = head;
            while (current != null)
            {
                if (current.Data.Equals(value))
                    return true;
                current = current.Next;
            }
            return false;
        }

    // 3.8 Delete an occurrence
    public bool DeleteOneOccurrence(T value)
        {
            DoubleNode<T>? current = head;
            while (current != null)
            {
                if (current.Data.Equals(value))
                {
                    if (head == tail) // Único nodo
                    {
                        head = tail = null;
                    }
                    else if (current == head) // Es la cabeza
                    {
                        head = head.Next;
                        if (head != null) head.Prev = null;
                    }
                    else if (current == tail) // Es la cola
                    {
                        tail = tail.Prev;
                        if (tail != null) tail.Next = null;
                    }
                    else // Está en medio
                    {
                        DoubleNode<T> prevNode = current.Prev!;
                        prevNode.Next = current.Next;
                        if (current.Next != null)
                        {
                            current.Next.Prev = prevNode;
                        }
                    }

                    count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

    // 3.9 Remove all occurrences
    public int DeleteAllOccurrences(T value)
        {
            int deletedCount = 0;
            while (DeleteOneOccurrence(value))
            {
                deletedCount++;
            }
            return deletedCount;
        }
    }
