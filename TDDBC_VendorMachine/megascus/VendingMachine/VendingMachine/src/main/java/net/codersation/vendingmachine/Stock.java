/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.codersation.vendingmachine;

import java.util.Objects;

/**
 * ひとつの商品の在庫を管理します。
 * @author megascus
 */
public class Stock <T extends Product> {
    private int id;
    private T product;
    private int count;
    
    private static int idnum = 0;

    public Stock(T product, int count) {
        this.id = idnum++;
        this.product = product;
        this.count = count;
    }
    
    /**
     * 自動販売機が商品を管理するIDです。この値は重複をしません。
     * @return 
     */
    public int getId() {
        return this.id;
    }
    
    public T getProduct() {
        return this.product;
    }

    public int getCount() {
        return count;
    }

    void setCount(int count) {
        this.count = count;
    }
    
    /**
     * 在庫を一つ減らします。
     */
    void decrement() {
        count--;
    }

    @Override
    public int hashCode() {
        int hash = 7;
        hash = 67 * hash + Objects.hashCode(this.product);
        hash = 67 * hash + this.count;
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
        final Stock<T> other = (Stock<T>) obj;
        if (!Objects.equals(this.product, other.product)) {
            return false;
        }
        if (this.count != other.count) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "Stock{" + "product=" + product + ", count=" + count + '}';
    }
}
