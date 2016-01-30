using System;
using System.Collections.Generic;
using System.Text;

using Props = System.Collections.Generic.Dictionary<string, object>;

namespace Bolt
{
	public class ReactiveEntity : Entity
	{

		public ReactiveEntity (float x = 0, float y = 0) : base(x, y)
		{
			
		}

		protected virtual Props defaultProps() {
			return new Props();
		}

		public void notifyChildren(Props props) {
			foreach (var e in GetChildren<ReactiveEntity>()) {
				(e as ReactiveEntity).notify(props);
			}
		}

		public virtual Props transform(Props props) {
			return props;
		}

		public void notify(Props props) {
			Props newProps = transform(props);
			onNewData(newProps);
			notifyChildren(newProps);
		}

		protected virtual void onNewData(Props data) {

		}

		public static String propsString(Props props) {
			StringBuilder sb = new StringBuilder();

			if (props == null) {
				return "{}";
			}

			foreach (KeyValuePair<string, object> kvp in props)
			{
				sb.Append(string.Format("[{0}: {1}], ", kvp.Key, kvp.Value));
			}

			return sb.ToString();
		}
	}
}

