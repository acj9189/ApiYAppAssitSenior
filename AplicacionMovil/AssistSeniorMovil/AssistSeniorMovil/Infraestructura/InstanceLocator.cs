using System;
using System.Collections.Generic;
using System.Text;
using AssistSeniorMovil.ViewModel;
namespace AssistSeniorMovil.Infraestructura
{
	public class InstanceLocator
	{
		public MainViewModel Main { get; set; }
		public InstanceLocator()
		{
			this.Main = new MainViewModel();
		}
	}
}
