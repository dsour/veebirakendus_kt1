<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
      <html>
        <head></head>
        <body>
          <h1 style="font-family:Arial">Sõiduplaanid</h1>
          <table style="width:650px">
            <tr>
              <xsl:for-each select="/Plaan/Liinid/Liin">
                <th style="text-align:left; vertical-align:top">
                  <xsl:for-each select="Suund">
                    <xsl:value-of select="."/>
                    <br/>
                  </xsl:for-each>
                </th>
              </xsl:for-each>
            </tr>
            <tr>
              <xsl:for-each select="/Plaan/Liinid/Liin">
                <td>
                  <table style="position:absolute">
                    <xsl:for-each select="PeatusedLiinil/PeatusLiinil">
                      <tr>
                        <td>
                          <xsl:value-of select="Nimetus"/>
                        </td>
                        <td>
                          <xsl:value-of select="Aeg"/>
                        </td>
                      </tr>
                    </xsl:for-each>
                  </table>
                </td>
              </xsl:for-each>
            </tr>            
          </table>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>
