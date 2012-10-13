package com.codersation.abe.vendingmachine.managestock;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author mao
 */
public class Juice {
    private String name;
    private Integer price;
    
    public Juice(String name, Integer price) {
        this.name = name;
        this.price = price;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Integer getPrice() {
        return price;
    }

    public void setPrice(Integer price) {
        this.price = price;
    }
    
    @Override
    public boolean equals(Object that) {
        if(that instanceof Juice) {
            Juice thatJuice = (Juice)that;
            if(this.name.equals(thatJuice.getName())
                    && this.price.equals(thatJuice.getPrice())) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
        
    }

    @Override
    public int hashCode() {
        int hash = 7;
        hash = 67 * hash + (this.name != null ? this.name.hashCode() : 0);
        hash = 67 * hash + (this.price != null ? this.price.hashCode() : 0);
        return hash;
    }
    
}
