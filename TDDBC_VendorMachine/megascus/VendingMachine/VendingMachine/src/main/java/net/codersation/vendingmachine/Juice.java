/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.codersation.vendingmachine;

import java.util.Objects;

/**
 * ジュースです。
 * @author megascus
 */
public class Juice implements Product {
    
    public static final Juice COKE = new Juice("coke", 120);
    public static final Juice RED_BULL = new Juice("red bull", 200);
    
    private String name;
    private int price;

    public Juice(String name, int price) {
        this.name = name;
        this.price = price;
    }

    @Override
    public String getName() {
        return name;
    }
    
    @Override
    public int getPrice() {
        return this.price;
    }

    @Override
    public int hashCode() {
        int hash = 7;
        hash = 83 * hash + Objects.hashCode(this.name);
        hash = 83 * hash + this.price;
        return hash;
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (getClass() != obj.getClass()) {
            return false;
        }
        final Juice other = (Juice) obj;
        if (!Objects.equals(this.name, other.name)) {
            return false;
        }
        if (this.price != other.price) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "Juice{" + "name=" + name + ", price=" + price + '}';
    }

    
}
