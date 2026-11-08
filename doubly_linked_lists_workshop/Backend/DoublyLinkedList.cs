namespace Backend;

using System;
using System.Collections.Generic;

    public class DoublyLinkedList<T> where T : IComparable<T> 
    {
    private DoubleNode<T>? head;      // nodo cabeza
    private DoubleNode<T>? tail;         // nodo cola
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

        // 3.1 Adicionar: Inserción en orden ascendente/descendente automático
        public void Add(T data)
        {
            DoubleNode<T> newNode = new DoubleNode<T>(data);

            // Caso 1: Lista vacía
            if (head == null)
            {
                head = newNode;
                tail = newNode;
                count++;
                return;
            }

            DoubleNode<T>? current = head;

            // Recorremos mientras el nodo actual exista y su dato cumpla la condición de orden
            while (current != null && Compare(current.Data, data) <= 0)
            {
                current = current.Next;
            }

            // Caso 2: Insertar al inicio (si current sigue siendo head)
            if (current == head)
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }
            // Caso 3: Insertar al final (si current es null, significa que es mayor que todos)
            else if (current == null)
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            // Caso 4: Insertar en el medio (entre current.Prev y current)
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

        // 3.2 Mostrar hacia adelante
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

        // 3.3 Mostrar hacia atrás (usando Prev)
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

        // 3.4 Ordenar descendentemente (invierte bandera y punteros físicos)
        public void SortDescending()
        {
            if (head == null || head == tail) return;

            // Cambiamos el estado de ordenamiento
            ascending = !ascending;

            DoubleNode<T>? current = head;
            DoubleNode<T>? temp = null;

            while (current != null)
            {
                temp = current.Prev;
                current.Prev = current.Next;
                current.Next = temp;
                current = current.Prev; // Avanzamos por el puntero invertido
            }

            temp = head;
            head = tail;
            tail = temp;

            Console.WriteLine("La lista ha cambiado su orden a descendente.");
        }

    // 3.5 Mostrar la(s) moda(s)
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
    // 3.6 Mostrar gráfico de ocurrencias
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

        // 3.8 Eliminar una ocurrencia
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

        // 3.9 Eliminar todas las ocurrencias
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
