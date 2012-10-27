/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.codersation.vendingmachine;

/**
 * 自動販売機で販売できる商品です。
 * @author megascus
 */
public interface Product {
    /**
     * @return 商品名 
     */
    String getName();
    
    @Override
    int hashCode();
    @Override
    boolean equals(Object o);
}
