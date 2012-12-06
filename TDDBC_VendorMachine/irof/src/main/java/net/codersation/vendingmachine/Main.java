package net.codersation.vendingmachine;

import java.util.Scanner;

public class Main {

	public static void main(String... args) {
		System.out.println("Insert?> ");
		try (Scanner scanner = new Scanner(System.in)) {
			while (scanner.hasNext()) {
				String input = scanner.nextLine();
				System.out.println("credit: " + input);
				System.out.println("Insert?> ");
			}
		}
	}
}
