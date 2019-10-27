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
            <v-btn class="mx-2" fab dark color="teal" @click.stop="dialogProduct = true">
              <v-icon dark>mdi-format-list-bulleted-square</v-icon>
            </v-btn>
          </v-col>

           <v-col cols="12" sm="6" md="6">
            <v-text-field label="Cliente*" v-model="id"></v-text-field>
          </v-col>                     
          
          <v-col cols="12" sm="6" md="6">   
            <v-btn @click="searchByPhone()" class="mx-2" dark color="teal">
              Buscar Cliente
            </v-btn>
          </v-col>

          <!-- dialogo para mostrar los productos para agregar al detalle -->
          <v-dialog
            v-model="dialogProduct"
            persistent            
            max-width="1000px"
          >            
            <v-card>
              <v-card-title>
                <span>  Seleccione un Producto </span>
              </v-card-title>

              <v-card-text>
                <v-container>
                  
                  <v-col cols="12" sm="12" md="12">
                    <v-text-field 
                      append-icon="search" 
                      class="text-xs-center" 
                      label="Nombre del Producto" 
                      @keyup.enter="listProducts()" 
                      v-model="text"
                    >
                    </v-text-field>
                  </v-col>

                  <v-col cols="12" sm="12" md="12">
                    <v-data-table        
                      :headers="headersProducts"
                      :items="products"
                      :search="search"
                      class="elevation-1"         
                    >     
                      <template v-slot:item.select="{item}"> 
                        <v-btn text small @click="addDetail(item)">
                          <v-icon>add</v-icon>                                              
                        </v-btn>               
                      </template>                                                                                       
                    
                      <template v-slot:no-data>
                        <h3>No hay productos agregados a la lista</h3>
                      </template>
                    </v-data-table>                 
                  </v-col>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn @click="dialogProduct = false" color="red" text>
                  Cerrar
                </v-btn>
              </v-card-actions>
            </v-card>           
          </v-dialog>
                              
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

              <template v-slot:item.salePrice="{item}">                
                <v-text-field v-model="item.salePrice"></v-text-field>                    
              </template>

              <template v-slot:item.subtotal="{item}">                
                {{(item.amount * item.salePrice) - item.discount * item.amount }}                    
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

            <v-col>
              <p class="text-right"> <span class="font-weight-black">Total Unitario: </span> ${{totalUnit = calculateUnitTotal}}</p>
            </v-col>

            <v-col>
              <p class="text-right font-weight-black"><strong>Descuento:</strong> ${{totalDiscount = calculateDiscount}}</p>
            </v-col>

            <v-col>
              <p class="text-right font-weight-bold"><strong>Total Neto:</strong> ${{total = calculateTotal}}</p>
            </v-col>

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
          {text:'Costo Unitario', value:'unitPrice', sortable: false},
          {text:'Precio', value:'salePrice', sortable: false},
          {text:'Subtotal', value:'subtotal', sortable: false},
          {text:'Borrar', value:'borrar', sortable: false}
        ],
        headersProducts:[    
          {text:'Seleccionar', value:'select', sortable: false },
          {text:'ID', value:'idProduct', sortable: false },
          {text:'Nombre', value:'productName'},
          {text:'Categoria', value:'category', sortable: false },
          {text:'Laboratorio', value:'laboratory', sortable: false },
          {text:'Stock', value:'stock', sortable: false },
          {text:'Precio Unit', value:'unitPrice'},
          {text:'Precio', value:'salePrice'}         
        ],
        typeSales:['FACTURA','BOLETA','TICKET'],
        typeSale:'',
        details:[],
        products:[],
        totalDiscount:0,
        totalUnit:0,
        total:0,
        text:'',
        voucherSeries:'',
        voucherNumber:'',
        discount:0,        
        nameProduct:'',
        search:'',               
        stock:0,        
        barCode:'',     
        price:'',
        estado:'',               
        dialogProduct:false
      }
    },
    created () {
     
    },
    computed: {
      calculateTotal() {
        let result = 0.0
        for (let i = 0; i < this.details.length; i++) {
          result = result + (this.details[i].salePrice * this.details[i].amount) 
                    - (this.details[i].amount * this.details[i].discount)            
        }
        return result 
      },
      calculateDiscount(){
        let result = 0.0
        for (let i = 0; i < this.details.length; i++) {
          result = result + (this.details[i].discount * this.details[i].amount) 
        }
        return result
      },
      calculateUnitTotal() {
        let result = 0.0
        for (let i = 0; i < this.details.length; i++) {
          result = result + (this.details[i].unitPrice * this.details[i].amount) 
        }
        return result
      }
    },
    methods: {
      listProducts(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/product/ListInSale/'+ me.text,headers)
        .then(function (response) {
          // handle success
          me.products = response.data                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);   
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }      
        })
      }, 
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
            unitPrice:data['unitPrice'],
            salePrice:data['salePrice']
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
      addNewSale(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.post('api/sale/Create/',
          {
            'IdUser':me.$store.state.user.idUser,
            'IdClient':'',            
            'TypeSale':me.typeSale,
            'VoucherSeries':me.voucherSeries,
            'VoucherNumber':me.voucherNumber,            
            'TotalPrice':me.total,           
            'Detail':me.details
          },
          headers)
        .then(function (response) {
          // handle success
          console.log('success') 
          me.listProducts()
          me.closeDialog()                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      deleteDetail(item){    
        this.details.splice(this.details.indexOf(item),1)              
      },
      clearSale(){
        this.typeSale = '',
        this.voucherSeries = '',
        this.voucherNumber = '',     
        this.totalDiscount = 0,
        this.discount = 0,
        this.totalUnit = 0,
        this.total = 0
        this.details = []
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

<style >
   p{
     margin:0px;
   }
</style>
