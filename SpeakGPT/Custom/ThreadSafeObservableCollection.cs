using System.Collections.ObjectModel;

namespace SpeakGPT.Custom
{
    public class ThreadSafeObservableCollection<T> : ObservableCollection<T>
    {
        private readonly object _lockObject = new();

        protected override void SetItem(int index, T item)
        {
            lock (_lockObject)
            {
                base.SetItem(index, item);
            }
        }

        protected override void InsertItem(int index, T item)
        {
            lock (_lockObject)
            {
                base.InsertItem(index, item);
            }
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            lock (_lockObject)
            {
                base.MoveItem(oldIndex, newIndex);
            }
        }

        protected override void RemoveItem(int index)
        {
            lock (_lockObject)
            {
                base.RemoveItem(index);
            }
        }

        protected override void ClearItems()
        {
            lock (_lockObject)
            {
                base.ClearItems();
            }
        }
    }
}
