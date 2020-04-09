using System.Collections.Generic;
using System.Linq;

namespace CFT.SignalR
{
    public class ConnectionMapping<T>
    {
        private static readonly Dictionary<T, HashSet<string>> Connections = new Dictionary<T, HashSet<string>>();

        public static int Count
        {
            get
            {
                return Connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (Connections)
            {
                HashSet<string> connections;
                if (!Connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    Connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

		private IReadOnlyList<string> GetConnectionsStrings(T key)
		{
			HashSet<string> connections;
			if (Connections.TryGetValue(key, out connections))
			{
				return connections.ToList();
			}

			return (IReadOnlyList<string>)Enumerable.Empty<string>();
		}

		public IReadOnlyList<string> GetConnectionsStrings(List<T> dialogUsers)
		{
            List<string> connections = new List<string>();

            foreach (var item in dialogUsers)
            {
                connections.AddRange(GetConnectionsStrings(item));
            }            

            return connections;
        }

		public bool GetConnections(T key)
        {
            if (Connections.TryGetValue(key, out _))
            {
                return true;
            }

            return false;
        }

        public void Remove(T key, string connectionId)
        {
            lock (Connections)
            {
                HashSet<string> connections;
                if (!Connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        Connections.Remove(key);
                    }
                }
            }
        }
    }
}
