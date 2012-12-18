package net.codersation.vendingmachine.money;

import java.util.Map;

public class ConsoleFormatter {
    String format(Map<Money, Integer> map) {
        StringBuilder sb = new StringBuilder();
        for (Map.Entry<Money, Integer> e : map.entrySet()) {
            if (sb.length() != 0) sb.append(", ");
            sb.append(e.getKey().getValue());
            sb.append("(").append(e.getValue()).append(")");
        }
        return sb.toString();
    }
}
