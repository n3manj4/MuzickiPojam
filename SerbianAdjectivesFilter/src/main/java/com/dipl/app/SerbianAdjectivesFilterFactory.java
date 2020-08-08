package com.dipl.app;

import java.util.Map;
import org.apache.lucene.analysis.TokenStream;
import org.apache.lucene.analysis.util.TokenFilterFactory;
public class SerbianAdjectivesFilterFactory extends TokenFilterFactory {
public SerbianAdjectivesFilterFactory(Map<String, String> args) {
 super(args);
 // TODO Auto-generated constructor stub
 }
@Override
 public TokenStream create(TokenStream input) {
 return new SerbianAdjectivesFilter(input);
 }
@Override
 public TokenStream normalize(TokenStream input) {
 // TODO Auto-generated method stub
 return super.normalize(input);
 }
}