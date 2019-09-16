//By Victoria Chen, 1272784
import java.io.FileReader;
import java.io.BufferedReader;

class XProcess {
public static void main(String [] args){
	String txtname; 
	BankBST accounts = new BankBST();
	//checks the correct arguement was input
	if (args.length!=1){
		System.err.println("Error: Incorrect input. java XProcess <filename.txt>");
		return;
	}
	txtname=args[0];
	try{
		BufferedReader br = new BufferedReader(new FileReader(args[0]));
		String s=br.readLine();
		int accountNum;
		String transType;
		float amount;			
		while(s!=null){
		String printStr="";
			String[] result=s.split("\\s");
			//checks that the split gives 3 results, otherwise end
			if (result.length!=3){
				System.err.println("Error: txt file not in correct format. <account number> <type> <amount>");
				return;
			}//end if
			//puts the values into the respective spots.
			accountNum=Integer.parseInt(result[0]);
			transType=result[1];
			amount=Float.parseFloat(result[2]);

			//checks that the account exists
			Account curr = accounts.find(accountNum);
			//if the account doesn't exist, add it to the tree
			if (curr==null){
				accounts.add(accountNum);
				//set the current to point to that account in the tree
				curr = accounts.find(accountNum);
			}//end if


			//gets the nodes to get to the target
			//start with the root node
			printStr=accounts.root.getKey()+"";
			//go through and add the nodes to get to destination to string
			Account output=accounts.root;
			while(output!=null && output.getKey()!=accountNum){
				if (accountNum<output.getKey()){
					output=output.left;
				}
				else{
					output=output.right;
				}
				printStr += " "+output.getKey();
			}
			//adds the type of transaction to end of string

			if (transType.equals("d"))
				printStr+=" DEPOSIT";
			else if (transType.equals("w"))
				printStr+=" WITHDRAW";
			else if (transType.equals("c"))
				printStr+=" CLOSE";
			//checks the type of transaction, and execute accordingly
			if(transType.equals("d") || transType.equals("w")){
				//deposit or withdrawal
				accounts.update(curr, transType, amount);
			}//end if
			else if(transType.equals("c")){
				//close the account
				accounts.remove(accountNum);
			}//end else if
			System.out.println(printStr);
			s=br.readLine();
		}//end while
		//print results
		System.out.println("RESULT");
		accounts.traverse(accounts.root);
		
	}//end try
	catch(Exception e){
		System.err.println(e);
	}//end catch
}//end main
}// end class xprocess
