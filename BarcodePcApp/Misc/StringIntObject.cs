using System;

namespace BarcodePcApp.Misc
{	
	/// <summary>

	///    This class defines the objects in the ComboBoxes that drive
	///    the properties of the color selection ComboBoxes.
	///    Use this object to use erations with ComboBoxes and ListBoxes.
	///    Add the <paramref term='s'/> data member to the eration item's
	///    english description and the <paramref term='i'/> data member to the actual
	///    value of the eration item.
	///    The ToString() method will allow the ComboBox and ListBox controls to 
	///    display the text which represents the eration item.

	/// </summary>
	/// 
	public class StringIntObject {
		public string m_sValue;
		public int m_nIndex; 
		public int m_nData;
		public int m_nData2;
		public double m_dData;
		public string m_sData;
		public DateTime m_dtDate;
		public DateTime m_dtEndDate;

		public StringIntObject(string sz, int n) {
			m_sValue=sz;
			m_nIndex=n;
			m_nData=0;
			m_nData2=0;
		}

		public StringIntObject(string sz, string sd) {
			m_sValue=sz;
			m_sData=sd;
			m_nData=0;
			m_nData2=0;
			m_nIndex=0;
		}

		public StringIntObject(string sz, int n, int d) {
			m_sValue=sz;
			m_nIndex=n;
			m_nData=d;
		}

		public StringIntObject(string sz, int n, int d, int d2, string sd) 
		{
			m_sValue=sz;
			m_nIndex=n;
			m_nData=d;
			m_nData2=d2;
			m_sData=sd;
		}

		public StringIntObject(string sz, int n, string sd) 
		{
			m_sValue=sz;
			m_nIndex=n;
			m_sData=sd;
		}

		public StringIntObject(string sz, int n, string sd, double d) {
			m_sValue=sz;
			m_nIndex=n;
			m_sData=sd;
			m_dData=d;
		}

		public StringIntObject(string sz, int n, DateTime d) {
			m_sValue=sz;
			m_nIndex=n;
			m_dtDate=d;
		}

		public StringIntObject(string sz, int n, DateTime d, DateTime dE) {
			m_sValue=sz;
			m_nIndex=n;
			m_dtDate=d;
			m_dtEndDate=dE;
		}

		public override string ToString() {
			return m_sValue;
		}
	}
}
