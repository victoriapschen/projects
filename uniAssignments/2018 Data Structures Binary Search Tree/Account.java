//By Victoria Chen, 1272784
	//defines the bank accounts
	class Account {
		//unique key to identify accounts
		private int key;
		//balance of the accounts
		private float balance;
		//left and right nodes
		public Account left,right;

		//constructor
		public Account(int n){
			key = n;
		}

		//returns the key
		public int getKey(){
			return key;
		}

		//returns the key
		public void setKey(int n){
			key=n;
		}

		//returns the balance
		public float getBalance(){
			return balance;
		}

		//sets the balance
		public void setBalance(float n){
			balance=n;
		}
	}//end class Account
	
