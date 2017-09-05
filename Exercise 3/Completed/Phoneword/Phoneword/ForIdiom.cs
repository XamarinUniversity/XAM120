using Xamarin.Forms;
using System;

namespace Core
{
	public static class ForIdiom
	{
		static bool CheckNullOrEmpty<T>(T value)
		{
			if (typeof(T) == typeof(string))
				return string.IsNullOrEmpty(value as string);

			return value == null || value.Equals(default(T));
		}

		/// <param name="phone">Action, if set, to execute for phone devices.</param>
		/// <param name="tablet">Action, if set, to execute for tablet devices. (Also used for desktop, when desktop Action not defined.)</param>
		/// <param name="desktop">Action, if set, to execute for desktop devices.</param>
		/// <param name="defaultValue">Action, if set, to execute when no other Idiom Action is executed.</param>
		public static void On(Action phone = null, Action tablet = null, Action desktop = null, Action defaultValue = null)
		{
			switch (Device.Idiom)
			{
				case TargetIdiom.Desktop:
					if (desktop != null)
					{
						desktop();
						return;
					}
					// If no desktop set, "fall through" to tablet-specific Action.
					goto case TargetIdiom.Tablet;
				case TargetIdiom.Tablet:
					if (tablet != null)
					{
						tablet();
						return;
					}
					break;
				case TargetIdiom.Phone:
					if (phone != null)
					{
						phone();
						return;
					}
					break;
			}
			if (defaultValue != null)
			{
				defaultValue();
			}
		}

		/// <param name="phone">Value, if set, to return for phone devices.</param>
		/// <param name="tablet">Value, if set, to return for tablet devices. (Also used for desktop, when desktop Action not defined.)</param>
		/// <param name="desktop">Value, if set, to return for desktop devices.</param>
		/// <param name="defaultValue">Value, or default of T, to return when no other Idiom parameter applies.</param>
		public static T On<T>(T phone = default(T), T tablet = default(T), T desktop = default(T), T defaultValue = default(T))
		{
			switch (Device.Idiom)
			{
				case TargetIdiom.Desktop:
					if (CheckNullOrEmpty(desktop))
					{
						return desktop;
					}
					goto case TargetIdiom.Tablet;
				case TargetIdiom.Tablet:
					if (CheckNullOrEmpty(tablet))
					{
						return tablet;
					}
					break;
				case TargetIdiom.Phone:
					return phone;
			}
			return phone;
		}
	}
	public class ForIdiom<T>
	{
		/// <summary>
		/// Value, or default of T, to return when no other Idiom parameter applies.
		/// </summary>
		public T DefaultValue { get; set; }
		/// <summary>
		/// Value, if set, to return for phone devices.
		/// </summary>
		public T Phone { get; set; }
		/// <summary>
		/// Value, if set, to return for tablet devices. (Also used for desktop, when desktop Action not defined.)
		/// </summary>
		public T Tablet { get; set; }
		/// <summary>
		/// Value, if set, to return for desktop devices.
		/// </summary>
		public T Desktop { get; set; }

		public static implicit operator T(ForIdiom<T> forPlatform)
		{
			return ForIdiom.On(forPlatform.Phone, forPlatform.Tablet, forPlatform.Desktop);
		}
	}
}
