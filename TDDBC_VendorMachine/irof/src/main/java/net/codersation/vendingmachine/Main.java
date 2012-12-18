package net.codersation.vendingmachine;

import net.codersation.vendingmachine.money.Money;

import java.util.Scanner;

public class Main {

    public static void main(String... args) {
        VendingMachine vendingMachine = new VendingMachine();
        System.out.print("> ");
        try (Scanner scanner = new Scanner(System.in)) {
            while (scanner.hasNext()) {
                String input = scanner.nextLine();
                if (input.startsWith("ins")) {
                    String[] split = input.split(" +");
                    if (split.length > 1) {
                        String insAmount = split[1];
                        try {
                            int amount = Integer.parseInt(insAmount);
                            boolean flg = false;
                            for (Money money : Money.values()) {
                                if (money.getValue() == amount) {
                                    flg = true;
                                    vendingMachine.insert(money);
                                    System.out.printf("money: %d was received.%n", amount);
                                    break;
                                }
                            }
                            if (!flg) {
                                System.out.printf("! %s is not available.%n", insAmount);
                            }
                        } catch (NumberFormatException e) {
                            System.out.printf("! %s is not available.%n", insAmount);
                        }
                    }
                } else if (input.equals("eject")) {
                    System.out.printf("100(1) was ejected.%n");
                }
                System.out.print("> ");
            }
        }
    }
}
