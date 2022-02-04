package Lab1;
import java.util.*;

public class Main {
    public static void main(String[] args) {

        Scanner scanner = new Scanner(System.in);
        String str;
        HashMap<Character, HashMap<Character, Character>> FiniteAutomata = new HashMap<>();

        System.out.println("Input the derivation rules:  ");
        while (true) {
            str = scanner.nextLine();

            if (str.equals("end"))
                break;
            
            String[] parts = str.split(" ");

            if (FiniteAutomata.containsKey(parts[0].charAt(0))) {
                if (parts[1].length() == 2) {
                    FiniteAutomata.get(parts[0].charAt(0)).put(parts[1].charAt(0), parts[1].charAt(1));
                } else {
                    FiniteAutomata.get(parts[0].charAt(0)).put(parts[1].charAt(0), '$');
                }

            } else {
                //filling up the hashmap
                HashMap<Character, Character> small = new HashMap<>();
                if (parts[1].length() == 2) {
                    small.put(parts[1].charAt(0), parts[1].charAt(1));
                } else {
                    small.put(parts[1].charAt(0), '$');
                }
                FiniteAutomata.put(parts[0].charAt(0), small);
            }
        }

        System.out.println("Print the string to be verified: ");
        str = scanner.nextLine();
        char first = 'S';

        for(int i = 0; i < str.length(); i++) {
            if(FiniteAutomata.get(first).containsKey(str.charAt(i))) {
                first = FiniteAutomata.get(first).get(str.charAt(i));
            }

            if(i == str.length() - 1) {
                if(first == '$')
                {
                    System.out.println("The string '" + str + "' is according to the rules");
                }
                else
                    System.out.println("The string '" + str + "' is not according to rules");
            }
        }

    }
}