namespace Backend;

public class SinglyLinkedList <T> // simplemente ligada de nodos, es decir, una lista de nodos
{
    private Node<T>? _head;

    public SinglyLinkedList()
    {
        _head = null;
    }


    public void BeginInsertAt(T data)
    {
        var newNode = new Node<T>(data);
        newNode.Next = _head;
        _head = newNode; 
    }

    public void EndInsertAt(T data)
    {
        var newNode = new Node<T>(data); // creamos un nuevo nodo con el dato que queremos insertar
        if (_head == null)
        {
            _head = newNode;
            return;
        }
        var current = _head;      // apuntador de la lista, es decir, apunta al primer nodo de la lista
        while (current.Next != null) // sea diferente de null , es decir, mientras haya un nodo siguiente, seguimos avanzando
        {
            current = current.Next; // avanzamos al siguiente direccion de memoria, es decir, al siguiente nodo
        }
        current.Next = newNode;
    }
    public override string ToString()
    { 
         var output = string.Empty; 
         var current = _head;
        while (current != null)
        {
            output += $"{current.Data} -> ";
            current = current.Next;
        }
        return $"{output} null ";


    }

}
