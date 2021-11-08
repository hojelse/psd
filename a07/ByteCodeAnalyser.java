import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Scanner;
import java.util.Set;

import javax.management.RuntimeErrorException;

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

            ArrayList<String> lines = new ArrayList<>();

            int i = 0;
            while(i < bytes.length) {
                String lineNumber = "#"+i;

                String instructionString = "";
                String[] nextTokens = getNextTokens(bytes, i);
                for (String token : nextTokens)
                    instructionString += token + " ";

                String nextBytes = "";
                for (int j = 0; j < nextTokens.length; j++) {
                    nextBytes += bytes[i+j] + " ";
                }

                System.out.println(lineNumber + "\t" + nextBytes + "\t\t" + instructionString);

                i += nextTokens.length;
            }
            
            s.close();
        }
        catch (FileNotFoundException fnfe) {
            System.err.println("No such file: " + args[0]);
        }
    }

    public static String[] getNextTokens(String[] bytes, int i) {
        int instruction = Integer.parseInt(bytes[i]);

        switch(instruction) {
            case CSTI:
                return new String[] { "CSTI", bytes[i+1] };
            case ADD:
                return new String[] { "ADD" };
            case SUB: 
                return new String[] { "SUB" };
            case MUL:
                return new String[] { "MUL" };
            case DIV:
                return new String[] { "DIV" };
            case MOD:
                return new String[] { "MOD" };
            case EQ:
                return new String[] { "EQ" };
            case LT:
                return new String[] { "LT" };
            case NOT:
                return new String[] { "NOT" };
            case DUP:
                return new String[] { "DUP" };
            case SWAP:
                return new String[] { "SWAP" };
            case LDI:
                return new String[] { "LDI" };
            case STI:
                return new String[] { "STI" };
            case GETBP:
                return new String[] { "GETBP" };
            case GETSP:
                return new String[] { "GETSP" };
            case INCSP:
                return new String[] { "INCSP", bytes[i+1] };
            case GOTO:
                return new String[] { "GOTO", "#"+bytes[i+1] };
            case IFZERO:
                return new String[] { "IFZERO", "#"+bytes[i+1] };
            case IFNZRO:
                return new String[] { "IFNZRO", "#"+bytes[i+1] };
            case CALL:
                return new String[] { "CALL", bytes[i+1], "#"+bytes[i+2] };
            case TCALL:
                return new String[] { "TCALL", bytes[i+1], bytes[i+2], "#"+bytes[i+3] };
            case RET:
                return new String[] { "RET", bytes[i+1] };
            case PRINTI:
                return new String[] { "PRINTI" };
            case PRINTC:
                return new String[] { "PRINTC" };
            case LDARGS:
                return new String[] { "LDARGS" };
            case STOP:
                return new String[] { "STOP" };
            default:
                throw new RuntimeException("Unknown instruction: " +  instruction);
        }
    }

    public static String getBytes(String[] bytes, int i, int count) {
        String str = "";
        for (int j = 0; j < count; j++) {
            str += bytes[j] + " ";
        }

        return str += "\t\t";
    }
}