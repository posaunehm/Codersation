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
    
    public static final Juice COKE = new Juice("coke");
    public static final Juice RED_BULL = new Juice("red bull");
    
    private String name;

    public Juice(String name) {
        this.name = name;
    }

    @Override
    public String getName() {
        return name;
    }

    @Override
    public int hashCode() {
        int hash = 7;
        hash = 97 * hash + Objects.hashCode(this.name);
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
        return true;
    }

    @Override
    public String toString() {
        return "Juice{" + "name=" + name + '}';
    } 
}
