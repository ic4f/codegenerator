using System;

namespace CodeGeneratorTest
{
	public class BaseTest
	{
		public BaseTest()
		{
			connection = "server=134.161.207.3;database=AutomatedTesting;user id=webadmin;password=z99b@7;";		
			//connection = "server=ADVWEB2;database=AutomatedTesting;uid=sergei;pwd=sacramento;";
			//connection = "server=DEEPTHOUGHT;database=AutomatedTesting;uid=tester;pwd=testme8726;";
		}

		public string Connection { get { return connection; } }

		private string connection;
	}
}
