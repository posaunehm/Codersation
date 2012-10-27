/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.codersation.vendingmachine;

import java.util.HashMap;
import java.util.Iterator;

/**
 * 複数の商品の在庫を管理します。
 * @author megascus
 */
public class Stocks implements Iterable<Stock<Product>> {
    
    private HashMap<Product, Stock<Product>> stocks = new HashMap<>();
    
    /**
     * 単一商品の在庫を取得します。
     * @param product 商品
     * @return 在庫
     */
    public Stock<Product> get(Product product) {
        return stocks.get(product);
    }
    
    /**
     * 在庫を追加します。
     * @param s 在庫
     */
    public void add(Stock<Product> s) {
        stocks.put(s.getProduct(), s);
    }

    @Override
    public Iterator<Stock<Product>> iterator() {
        return stocks.values().iterator();
    }
    
    
    
}
