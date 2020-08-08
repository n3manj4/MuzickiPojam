package com.dipl.app;

import java.io.FileWriter;
import java.io.IOException;
import org.apache.lucene.analysis.TokenFilter;
import org.apache.lucene.analysis.TokenStream;
import org.apache.lucene.analysis.tokenattributes.CharTermAttribute;
import org.apache.lucene.analysis.tokenattributes.PositionIncrementAttribute;

public class SerbianAdjectivesFilter extends TokenFilter {

    private final CharTermAttribute termAtt;
    private final PositionIncrementAttribute posIncrAtt;
    private int curPosIncr;
    private boolean isfirst = true;

    private char[][] suffixes;
    private int numberOfSuffixes = 11;
    private int suffixLength = 3;
    private int currentSuffix = 0;

    private char[] curTermBuffer;
    private char[] newBuffer;
    private int bufferLength;
    private int curTermLength;

    protected SerbianAdjectivesFilter(TokenStream ts) {
        super(ts);
        
        initSuffixes();

        this.termAtt = addAttribute(CharTermAttribute.class);
        this.posIncrAtt = addAttribute(PositionIncrementAttribute.class);
    }

    @Override
    public boolean incrementToken() throws IOException {
    while (true)
    {
        if (curTermBuffer == null) {
            if (!input.incrementToken()) {
              return false;
            }

            curTermLength = termAtt.length();
            curPosIncr += posIncrAtt.getPositionIncrement();
            bufferLength = curTermLength + 3;
            
            curTermBuffer = new char[curTermLength];
            curTermBuffer = termAtt.buffer().clone();
            newBuffer = new char[bufferLength];
        }
        
        
        if (isfirst)
        {
            for (int i = 0; i < curTermLength; i++) {
                newBuffer[i] = curTermBuffer[i];
            }   
            
            posIncrAtt.setPositionIncrement(curPosIncr);
            curPosIncr = 0;
            termAtt.copyBuffer(newBuffer, 0, curTermLength);
            
            isfirst = false;
            return true;
        }
        
        
        for (int i = currentSuffix; i < numberOfSuffixes; i++) {
            for (int j = 0; j < suffixLength; j++)
            {
                if (suffixes[i][j] == '-') {
                    currentSuffix++;
                    posIncrAtt.setPositionIncrement(curPosIncr);
                    termAtt.copyBuffer(newBuffer, 0, bufferLength - 1);
                    return true;
                }
                newBuffer[curTermLength + j] = suffixes[i][j];
            }
            currentSuffix++;
            posIncrAtt.setPositionIncrement(curPosIncr);
            termAtt.copyBuffer(newBuffer, 0, bufferLength);
            return true;
        }  

        if (numberOfSuffixes == currentSuffix) {
            curTermBuffer = null;
        }
    }
}

@Override
    public void reset() throws IOException {
      super.reset();
      curTermBuffer = null;
      curPosIncr = 0;
    }
  
    @Override
    public void end() throws IOException {
      super.end();
      posIncrAtt.setPositionIncrement(curPosIncr);
    }

    private void initSuffixes()
    {
        suffixes = new char[numberOfSuffixes][suffixLength];
        
        suffixes[0][0] = 's';
        suffixes[0][1] = 'k';
        suffixes[0][2] = 'i';

        suffixes[1][0] = 'č';
        suffixes[1][1] = 'k';
        suffixes[1][2] = 'i';

        suffixes[2][0] = 'i';
        suffixes[2][1] = 'j';
        suffixes[2][2] = 'i';

        suffixes[3][0] = 'n';
        suffixes[3][1] = 'a';
        suffixes[3][2] = 't';

        suffixes[4][0] = 'o';
        suffixes[4][1] = 'v';
        suffixes[4][2] = '-';

        suffixes[5][0] = 'a';
        suffixes[5][1] = 'n';
        suffixes[5][2] = '-';

        suffixes[6][0] = 'a';
        suffixes[6][1] = 't';
        suffixes[6][2] = '-';

        suffixes[7][0] = 'e';
        suffixes[7][1] = 'v';
        suffixes[7][2] = '-';

        suffixes[8][0] = 'i';
        suffixes[8][1] = 'n';
        suffixes[8][2] = '-';

        suffixes[9][0] = 'n';
        suffixes[9][1] = 'i';
        suffixes[9][2] = '-';

        suffixes[10][0] = 'j';
        suffixes[10][1] = 'i';
        suffixes[10][2] = '-';
    }
}