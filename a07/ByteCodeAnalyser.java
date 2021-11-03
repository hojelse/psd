import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashSet;
import java.util.Scanner;
import java.util.Set;

/**
 * ByteCodeAnalyser
 */
public class ByteCodeAnalyser {
    final static int
    CSTI = 0, ADD = 1, SUB = 2, MUL = 3, DIV = 4, MOD = 5, 
    EQ = 6, LT = 7, NOT = 8, 
    DUP = 9, SWAP = 10, 
    LDI = 11, STI = 12, 
    GETBP = 13, GETSP = 14, INCSP = 15, 
    GOTO = 16, IFZERO = 17, IFNZRO = 18, CALL = 19, TCALL = 20, RET = 21, 
    PRINTI = 22, PRINTC = 23, 
    LDARGS = 24,
    STOP = 25;

    public static void main(String[] args) {
        try {
            File file = new File(args[0]);
            Scanner s = new Scanner(file);
            var bytes = s.nextLine().split(" ");

            Set<Integer> labels = new HashSet<>();

            for(int i = 0; i < bytes.length; i++) {
                System.out.print("#"+i+" ");
                int inst = Integer.parseInt(bytes[i]);
                switch(inst) {
                    case 0:
                        System.out.println("CSTI " + bytes[++i] ); break;
                    case 1:
                        System.out.println("ADD"); break;
                    case 2: 
                        System.out.println("SUB"); break;
                    case 3:
                        System.out.println("MUL"); break;
                    case 4:
                        System.out.println("DIV"); break;
                    case 5:
                        System.out.println("MOD"); break;
                    case 6:
                        System.out.println("EQ"); break;
                    case 7:
                        System.out.println("LT"); break;
                    case 8:
                        System.out.println("NOT"); break;
                    case 9:
                        System.out.println("DUP"); break;
                    case 10:
                        System.out.println("SWAP"); break;
                    case 11:
                        System.out.println("LDI"); break;
                    case 12:
                        System.out.println("STI"); break;
                    case 13:
                        System.out.println("GETBP"); break;
                    case 14:
                        System.out.println("GETSP"); break;
                    case 15:
                        System.out.println("INCSP " + bytes[++i]); break;
                    case 16:
                        System.out.println("GOTO #" + bytes[++i]); labels.add(Integer.parseInt(bytes[i])); break;
                    case 17:
                        System.out.println("IFZERO #" + bytes[++i]); labels.add(Integer.parseInt(bytes[i])); break;
                    case 18:
                        System.out.println("IFNZRO #" + bytes[++i]); labels.add(Integer.parseInt(bytes[i])); break;
                    case 19:
                        System.out.println("CALL " + bytes[++i] + " #" + bytes[++i]); labels.add(Integer.parseInt(bytes[i])); break;
                    case 20:
                        System.out.println("TCALL " + bytes[++i] + " " + bytes[++i] + " #" + bytes[++i]); labels.add(Integer.parseInt(bytes[i])); break;
                    case 21:
                        System.out.println("RET " + bytes[++i]); break;
                    case 22:
                        System.out.println("PRINTI"); break;
                    case 23:
                        System.out.println("PRINTC"); break;
                    case 24:
                        System.out.println("LDARGS"); break;
                    case 25:
                        System.out.println("STOP"); break;
                }
            }
            
            s.close();
        }
        catch (FileNotFoundException fnfe) {
            System.err.println("No such file: " + args[0]);
        }
    }
}