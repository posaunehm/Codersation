package net.codersation.vendingmachine;

import net.codersation.vendingmachine.money.Money;
import net.codersation.vendingmachine.money.MoneyStock;
import net.codersation.vendingmachine.report.PurchasableReport;

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
                        for (int i = 1; i < split.length; i++) {
                            String insAmount = split[i];
                            try {
                                int count = 1;
                                if (insAmount.contains("x")) {
                                    count = Integer.valueOf(insAmount.split("x")[1]);
                                    if (count > 100) {
                                        System.out.printf("! %s is not available.%n", insAmount);
                                        count = 0;
                                    } else {
                                        insAmount = insAmount.split("x")[0];
                                    }
                                }
                                if (count > 0) {
                                    for (int j = 0; j < count; j++) {
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
                                    }
                                }
                            } catch (NumberFormatException e) {
                                System.out.printf("! %s is not available.%n", insAmount);
                            }
                        }
                    }
                } else if (input.equals("eject")) {
                    vendingMachine.payBack();
                    MoneyStock change = vendingMachine.takeOutChange();
                    System.out.println(change.getText() + " was ejected.");
                } else if (input.equals("exit")) {
                    break;
                } else if (input.equals("info")) {
                    System.out.printf("Credit:%d%n", vendingMachine.getCreditAmount());
                    PurchasableReport report = vendingMachine.getPurchasable();
                    System.out.println(report.toString());
                }
                System.out.print("> ");
            }
        }
    }
}
