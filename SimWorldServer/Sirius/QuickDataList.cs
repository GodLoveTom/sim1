using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickData
{
    public class QDListNode<T>
    {
        public T Data { set; get; }          //数据域,当前结点数据
        public QDListNode<T> Next { set; get; }    //位置域,下一个结点地址
        public QDListNode<T> Pre { set; get; }    //位置域,下一个结点地址

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="item"></param>
        public QDListNode(T item)
        {
            this.Data = item;
            this.Next = null;
            this.Pre = null;
        }
        /// <summary>
        /// 构造
        /// </summary>
        public QDListNode()
        {
            this.Data = default(T);
            this.Next = null;
            this.Pre = null;
        }
    }

    public class QDList<T>
    {
        public QDListNode<T> Head { set; get; } //链表头
        public QDListNode<T> Tail { set; get; } //链表尾
        public int count = 0;

        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// 构造
        /// </summary>
        public QDList()
        {
            Head = null;
            Tail = null;
        }

        public IEnumerator<QDListNode<T>> GetEnumerator()
        {
            QDListNode<T> node = Head;
            while (node != null)
            {
                yield return node;
                node = node.Next;
            }
        }

        public QDListNode<T> FirstNode()
        {
            return Head;
        }

        public QDListNode<T> Next(QDListNode<T> node)
        {
            return node.Next;
        }

        /// <summary>
        /// 重载[] 效率很低,谨慎使用
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public T this[int idx]
        {
            get
            {
                if (idx >= 0 && idx < count)
                {
                    QDListNode<T> one = Head;
                    for (int i = 0; i < idx; i++)
                    {
                        one = one.Next;
                    }

                    return one.Data;
                }
                else
                {
                    throw new System.IndexOutOfRangeException();
                }
            }
        }

        public void ClearAll()
        {
            QDListNode<T> node = Head;
            QDListNode<T> next = Head;
            while (next != null)
            {
                node = next;
                next = next.Next;

                node.Pre = null;
                node.Next = null;
            }

            count = 0;
            Head = null;
            Tail = null;
        }

        /// <summary>
        /// 删除顺序中的某个数据；效率很低,谨慎使用
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < count)
            {
                QDListNode<T> one = Head;
                for (int i = 0; i < index; i++)
                {
                    one = one.Next;
                }
                DelNode(one);
            }
        }


        /// <summary>
        /// 默认放到队尾
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            AppendTail(item);
        }

        /// <summary>
        /// 增加新元素到链表末尾
        /// </summary>
        public void AppendTail(T item)
        {
            QDListNode<T> it = new QDListNode<T>(item);

            if (Head == null)
            {
                Head = it;
                Tail = it;
            }
            else
            {
                Tail.Next = it;
                it.Pre = Tail;
                Tail = it;
            }

            count++;
        }

        /// <summary>
        /// 插入头
        /// </summary>
        /// <param name="item"></param>
        public void AppendHead(T item)
        {
            QDListNode<T> it = new QDListNode<T>(item);

            if (Head == null)
            {
                Head = it;
                Tail = it;
            }
            else
            {
                it.Next = Head;
                Head.Pre = it;
                Head = it;
            }

            count++;
        }

        /// <summary>
        /// 删除一个节点
        /// </summary>
        /// <param name="node">节点</param>
        public void DelNode(QDListNode<T> node)
        {
            if (node == null)
                return;

            count--;

            if (node.Pre != null)
            {
                node.Pre.Next = node.Next;
                if (node.Pre.Next == null)
                    Tail = node.Pre;
            }
            else
                Head = node.Next;

            if (node.Next != null)
            {
                node.Next.Pre = node.Pre;
                if (node.Next.Pre == null)
                    Head = node.Next;
            }
            else
                Tail = node.Pre;
        }

        public void InsertBeforeNode(QDListNode<T> node, T item)
        {
            QDListNode<T> it = new QDListNode<T>(item);
            count++;

            if (node.Pre == null)
                Head = it;
            else
            {
                node.Pre.Next = it;
                it.Pre = node.Pre;
            }

            node.Pre = it;
            it.Next = node;
        }

        public void InsertAfterNode(QDListNode<T> node, T item)
        {
            QDListNode<T> it = new QDListNode<T>(item);
            count++;

            if (node.Next == null)
                Tail = it;
            else
            {
                node.Next.Pre = it;
                it.Next = node.Next;
            }

            node.Next = it;
            it.Pre = node;
        }

        public T GetAndDelOldOne()
        {
            if (Head != null)
            {
                QDListNode<T> one = Head;
                DelNode(Head);
                return one.Data;
            }
            else
                return default(T);
        }

        public T GetFirst()
        {
            if (Head != null)
                return Head.Data;
            else
                return default(T);
        }

        public T GetFirstAndRemove()
        {
            if (Head != null)
            {
                QDListNode<T> one = Head;
                DelNode(Head);
                return one.Data;
            }
            else
                return default(T);
        }


    }




}
