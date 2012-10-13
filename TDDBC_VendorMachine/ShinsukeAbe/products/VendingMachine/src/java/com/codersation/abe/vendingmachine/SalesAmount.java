/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.codersation.abe.vendingmachine;

/**
 *
 * @author mao
 */
class SalesAmount {
    private Integer totalAmount = 0;
    
    public Integer getTotal() {
        return totalAmount;
    }

    public void add(Integer amount) {
        totalAmount += amount;
    }
}
