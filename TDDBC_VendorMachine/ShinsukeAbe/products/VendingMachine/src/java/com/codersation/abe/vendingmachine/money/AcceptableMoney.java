package com.codersation.abe.vendingmachine.money;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author mao
 */
public class AcceptableMoney {
    private Integer value;
    
    AcceptableMoney(Integer value) {
        this.value = value;
    }
    
    public Integer getValue() {
        return value;
    }
    
    @Override
    public boolean equals(Object that) {
        if(that instanceof AcceptableMoney) {
            AcceptableMoney thatMoney = (AcceptableMoney) that;
            if(this.value.equals(thatMoney.getValue())) {
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
        int hash = 5;
        hash = 83 * hash + (this.value != null ? this.value.hashCode() : 0);
        return hash;
    }
}
