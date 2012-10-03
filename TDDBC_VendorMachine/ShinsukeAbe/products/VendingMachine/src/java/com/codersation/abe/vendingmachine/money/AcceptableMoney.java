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
}
