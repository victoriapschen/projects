//By Victoria Chen, 1272784
//bank binary search tree
class BankBST{

	//root/head of the tree
	Account root;

	//Method to find account
	public Account find(int k){
		Account curr = root;
		while (curr!= null && curr.getKey()!=k){
			if(k<curr.getKey()){
				curr=curr.left;
			}
			else{
				curr=curr.right;
			}
		}//end while		
		return curr;
	}//end find method

	//method to add new accounts
	public void add(int k){
		Account a = new Account(k);
		//if there is no root then make this the new root
		if (root == null){
			root=a;
			return;
		}//end if
		Account curr = root;
		while (curr.getKey()!=k){
			if(k<curr.getKey()){
				if(curr.left==null){
					curr.left=a;
				}//end if
				else {
					curr=curr.left;
				}//end else
			}//end if
			else{
				if(curr.right==null){
					curr.right=a;
				}//end if
				else{
					curr=curr.right;
				}//end else
			}//end else
		}//end while
	}//end add

	//method to update balance
	public void update(Account curr, String t, float am){
		float currB = curr.getBalance();
		if (t.equals("d")){
			//add to the balance
			curr.setBalance(currB+am);
		}
		else{
			//withdraw from balance
			curr.setBalance(currB-am);
		}
	}

	//method to remove accounts
	public void remove(int k){
		//calls the recursive delete
		root = deleteAcc(root, k);
	}//end remove method

	//recursive function for deleteing
	public Account deleteAcc(Account root, int k){
		//if tree is empty, return
		if (root==null) {
			return root;
		}

		//otherwise it isn't empty so go down tree
		if (k<root.getKey()){
			root.left=deleteAcc(root.left, k);
		}
		else if(k>root.getKey()){
			root.right=deleteAcc(root.right, k);		
		}

		//If the key matches then this is the one that is going to be deleted.
		else{
			if(root.left==null){
				return root.right;
			}
			else if (root.right==null){
				return root.left;
			}

			//otherwise the node has two children
			//get the left most node in right subtree
			Account tmp = root.right;
			//keep looping until end of lefts
			while(tmp.left!=null){
				tmp=tmp.left;
			}
			//sets the account to be that one
			root.setKey(tmp.getKey());
			root.setBalance(tmp.getBalance());

			//delete the successor
			root.right = deleteAcc(root.right, root.getKey());
		}//end else
		return root;
	}//end delete method	

	public void traverse(Account curr){
		if (curr==null)
		return;
		
		traverse(curr.left);
		System.out.println(curr.getKey()+" "+curr.getBalance());
		traverse(curr.right);
	}//end traverse method

}//end class bankbst
