<template>

  <v-card>
    <v-card-title>
      <span class="headline">Nueva Venta</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
        
          <v-col cols="12" sm="4" md="4">
            <v-select
              v-model="typeSale"
              :items="typeSales"                                   
              label="Tipo Comprobante*"
              required
            ></v-select>                        
          </v-col>
          <v-col cols="12" sm="4" md="4">
            <v-text-field label="Serie de comprobante*" v-model="voucherSeries" required></v-text-field>
          </v-col>
          <v-col cols="12" sm="4" md="4">
            <v-text-field label="Numero de comprobante*" v-model="voucherNumber" required></v-text-field>
          </v-col>              
          
                    
          <v-col cols="12" sm="6" md="6">
            <v-text-field @keyup.enter="searchByBarCode()" label="Codigo de barra*" v-model="barCode"></v-text-field>
          </v-col>                     
          
          <v-col cols="12" sm="6" md="6">   
            <v-btn class="mx-2" fab dark color="teal">
              <v-icon dark>mdi-format-list-bulleted-square</v-icon>
            </v-btn>
          </v-col>

          <v-col cols="12" sm="12" md="12">
            <v-data-table        
              :headers="headersDataGridDetails"
              :items="details"
              :search="search"
              class="elevation-1"         
            >     
              <template v-slot:item.amount="{item}">                
                <v-text-field v-model="item.amount"></v-text-field>                    
              </template>

              <template v-slot:item.discount="{item}">                
                <v-text-field v-model="item.discount"></v-text-field>                    
              </template>

              <template v-slot:item.unitPrice="{item}">                
                <v-text-field v-model="item.unitPrice"></v-text-field>                    
              </template>

              <template v-slot:item.subtotal="{item}">                
                {{(item.amount * item.unitPrice) - item.discount * item.amount }}                    
              </template>
                                        
              <template v-slot:item.borrar="{ item }">         
                <v-icon
                  small
                  class="mr-2"                  
                  @click="deleteDetail(item)"
                >
                  delete
                </v-icon>                                                                                
              </template>  

            
              <template v-slot:no-data>
                <h3>No hay productos agregados al detalle</h3>
              </template>
            </v-data-table>

            <v-col cols="12" xs="12" sm="12" md="6">   
              <v-btn class="mx-2" dark color="blue darken-1" @click="cancel()">
                Cancelar
              </v-btn>
               <v-btn class="mx-2" dark color="success darken-1" @click="save()">
                Guardar
              </v-btn>
          </v-col>
          </v-col>
        </v-row>
      </v-container>
      <small>*indica campo obligatorio</small>
  
    </v-card-text>
 
  </v-card> 
   
</template>

<script>
 import axios from 'axios'  
  export default {
    props: {          
      
    },
    data() {
      return {
         headersDataGridDetails:[        
          {text:'Producto', value:'product', sortable: false},
          {text:'Cantidad', value:'amount', sortable: false},
          {text:'Descuento', value:'discount', sortable: false},
          {text:'Costo Unitario', value:'unitCost', sortable: false},
          {text:'Precio', value:'unitPrice', sortable: false},
          {text:'Subtotal', value:'subtotal', sortable: false},
          {text:'Borrar', value:'borrar', sortable: false}
        ],
        typeSales:['FACTURA','BOLETA','TICKET'],
        typeSale:'',
        details:[],
        voucherSeries:'',
        voucherNumber:'',
        discount:'',        
        nameProduct:'',
        search:'',               
        stock:0,        
        barCode:'',     
        price:'',
        estado:'',       
        menu1: false
      }
    },
    created () {
     
    },
    methods: {
      searchByBarCode() {
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/product/getByBarCode/'+this.barCode,headers)
        .then(function (response) {
          // handle success
          me.addDetail(response.data)                                                     
          })
        .catch(function (error) {
          // handle error          
          console.log(error);   
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }      
        })
      },
      addDetail(data = []){
        if (this.findInDetail(data['idProduct'])) 
        {
          alert("el producto ya ha sido agregado")
        } else {
          console.log(data)
          this.details.push({
            idProduct:data['idProduct'],
            product:data['productName'],
            amount:1,
            discount:0,
            unitCost:0,
            unitPrice:0
          })
        }
        
      },
      findInDetail(id){
        for (let i = 0; i < this.details.length; i++) {
          if(this.details[i].idProduct === id ){
            return true
          }          
        }               
        return false
      },
      deleteDetail(item){    
        this.details.splice(this.details.indexOf(item),1)              
      },
      save() {
        this.$emit('save',{ 
          
        })
      },
      cancel(){
        this.$emit('cancel',false)
      }
    },
  }
</script>

