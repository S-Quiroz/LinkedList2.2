
namespace Backend;

public  class Node<T>

{  public Node(T data)
    {
        Data = data;
        Next = null!;
        
    }

    public T? Data { get; set; }    

    public Node<T> Next { get; set; } // next es el apuntador de la misma clase Node, es decir, apunta a otro nodo de la misma clase

}
