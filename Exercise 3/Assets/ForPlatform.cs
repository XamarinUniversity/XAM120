using Xamarin.Forms;
using System;

namespace Core
{
	public static class ForPlatform
	{
		static bool CheckNullOrEmpty<T>(T value)
		{
			if (typeof(T) == typeof(string))
				return string.IsNullOrEmpty(value as string);

			return value == null || value.Equals(default(T));
		}

		/// <param name="iOS">Action, if set, to execute for iOS devices.</param>
		/// <param name="android">Action, if set, to execute for Android devices.</param>
		/// <param name="winPhone">Action, if set, to execute for Windows 8 Silverlight devices.</param>
		/// <param name="windowsStore">Action, if set, to execute for Windows 8.1, Windows Phone 8.1, and all UWP/Windows 10 devices.</param>
		/// <param name="defaultValue">Action, if set, to execute when no other platform parameter applies.</param>
		public static void On(Action iOS = null, Action android = null, Action winPhone = null, Action windowsStore = null, Action defaultValue = null)
		{
			switch (Device.OS)
			{
				case TargetPlatform.iOS:
					if (iOS != null)
					{
						iOS();
						return;
					}
					break;

				case TargetPlatform.Android:
					if (android != null)
					{
						android();
						return;
					}
					break;

				case TargetPlatform.WinPhone:
					if (winPhone != null)
					{
						winPhone();
						return;
					}
					break;

				case TargetPlatform.Windows:
					if (windowsStore != null)
					{
						windowsStore();
						return;
					}
					break;
			}
			if (defaultValue != null)
			{
				defaultValue();
			}
		}

		/// <param name="iOS">Value, if set, to return for iOS devices.</param>
		/// <param name="android">Value, if set, to return for Android devices.</param>
		/// <param name="winPhone">Value, if set, to return for Windows 8 Silverlight devices.</param>
		/// <param name="windowsStore">Value, if set, to return for Windows 8.1, Windows Phone 8.1, and all UWP/Windows 10 devices.</param>
		/// <param name="defaultValue">Value, or default of T, to return when no other platform parameter applies.</param>
		public static T On<T>(T iOS = default(T), T android = default(T), T winPhone = default(T), T windowsStore = default(T), T defaultValue = default(T))
		{
			switch (Device.OS)
			{
				case TargetPlatform.iOS:
					if (!CheckNullOrEmpty(iOS))
					{
						return iOS;
					}
					break;

				case TargetPlatform.Android:
					if (!CheckNullOrEmpty(android))
					{
						return android;
					}
					break;

				case TargetPlatform.WinPhone:
					if (!CheckNullOrEmpty(winPhone))
					{
						return winPhone;
					}
					break;

				case TargetPlatform.Windows:
					if (!CheckNullOrEmpty(windowsStore))
					{
						return windowsStore;
					}
					break;
			}
			return defaultValue;
		}
	}
	public class ForPlatform<T>
	{
		/// <summary>
		/// Value, or default of T, to return when no other platform parameter applies.
		/// </summary>
		public T DefaultValue { get; set; }
		/// <summary>
		/// Value, if set, to return for iOS devices.
		/// </summary>
		public T iOS { get; set; }
		/// <summary>
		/// Value, if set, to return for Android devices.
		/// </summary>
		public T Android { get; set; }
		/// <summary>
		/// Value, if set, to return for Windows 8 Silverlight devices.
		/// </summary>
		public T WinPhone { get; set; }
		/// <summary>
		/// Value, if set, to return for Windows 8.1, Windows Phone 8.1, and all UWP/Windows 10 devices.
		/// </summary>
		public T WindowsStore { set; get; }

		public static implicit operator T(ForPlatform<T> forPlatform)
		{
			return ForPlatform.On(forPlatform.iOS, forPlatform.Android, forPlatform.WinPhone, forPlatform.WindowsStore);
		}
	}
}
