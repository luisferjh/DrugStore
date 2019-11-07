<template>

  <v-card>
    <v-card-title>
      <span class="headline">Nueva Venta</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-col cols="12" sm="4" md="4">
            <v-text-field label="Tipo de venta*" v-model="typeSale" required></v-text-field>
          </v-col>
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
          <v-col cols="12" sm="6" md="6">
            <v-text-field label="Numero de comprobante*" v-model="voucherNumber" required></v-text-field>
          </v-col>              
          
          <v-col cols="12" sm="7" md="7">
            <v-text-field @keyup.enter="searchByPhoneNumber()" v-model="phoneNumber" label="Cliente*"></v-text-field>
          </v-col>                     
          
          <v-col cols="12" sm="5" md="5">   
            <v-btn class="mx-2" @click.stop="dialogClient = true" fab dark color="teal">
             <v-icon dark>mdi-format-list-bulleted-square</v-icon>
            </v-btn>
          </v-col>

          <v-col cols="12" sm="6" md="6">
            <v-text-field @keyup.enter="searchByBarCode()" label="Codigo de barra*" v-model="barCode"></v-text-field>
          </v-col>                     
          
          <v-col cols="12" sm="6" md="6">   
            <v-btn class="mx-2" fab dark color="teal" @click.stop="dialogProduct = true">
              <v-icon dark>mdi-format-list-bulleted-square</v-icon>
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

             <!-- dialogo para mostrar los clientes para agregar al detalle -->
          <v-dialog
            v-model="dialogClient"
            persistent            
            max-width="1000px"
          >            
            <v-card>
              <v-card-title>
                <span>  Seleccione un Cliente </span>
              </v-card-title>

              <v-card-text>
                <v-container>
                  
                  <v-col cols="12" sm="12" md="12">
                    <v-text-field 
                      append-icon="search" 
                      class="text-xs-center" 
                      label="Nombre del Cliente" 
                      @keyup.enter="listClients()" 
                      v-model="fullName"
                    >
                    </v-text-field>
                  </v-col>

                  <v-col cols="12" sm="12" md="12">
                    <v-data-table        
                      :headers="headersClients"
                      :items="clientsByName"                     
                      class="elevation-1"         
                    >     
                      <template v-slot:item.select="{item}"> 
                        <v-btn text small @click="">
                          <v-icon>add</v-icon>                                              
                        </v-btn>               
                      </template>                                                                                       
                    
                      <template v-slot:no-data>
                        <h3>No hay clientes agregados a la lista</h3>
                      </template>
                    </v-data-table>                 
                  </v-col>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn @click="dialogClient = false" color="red" text>
                  Cerrar
                </v-btn>
              </v-card-actions>
            </v-card>           
          </v-dialog>
          
          <!-- data table para mostrar el cliente en la venta (opcional) -->
          <v-col cols="12" sm="12" md="12">
            <v-data-table        
              :headers="headersClients"
              :items="clientByPhoneNumber"             
              class="elevation-1"         
            >                  
                                        
              <template v-slot:item.borrar="{ item }">         
                <v-icon
                  small
                  class="mr-2"                  
                  @click=""
                >
                  delete
                </v-icon>                                                                                
              </template>  
            
              <template v-slot:no-data>
                <h3>No hay clientes agregados a la venta</h3>
              </template>
            </v-data-table>
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
              <p class="text-left"> <span class="font-weight-black">Total Unitario: </span> ${{totalUnit = calculateUnitTotal}}</p>
            </v-col>

            <v-col>
              <p class="text-left font-weight-black"><strong>Descuento:</strong> ${{totalDiscount = calculateDiscount}}</p>
            </v-col>

            <v-col>
              <p class="text-left font-weight-bold"><strong>Total Neto:</strong> ${{total = calculateTotal}}</p>
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
        headersClients:[
          {text:'Seleccionar', value:'select', sortable: false },
          {text:'ID', value:'idClient'},
          {text:'Nombre', value:'name',sortable: false},
          {text:'Apellidos', value:'lastName',sortable: false},
          {text:'Tipo de Documento', value:'documentType',sortable: false},
          {text:'Numero De Documento', value:'documentNumber',sortable: false},
          {text:'Numero de telefono', value:'phoneNumber',sortable: false},
          {text:'Borrar', value:'borrar', sortable: false}            
        ],
        typeSales:['FACTURA','BOLETA','TICKET'],
        typeSale:'',
        details:[],
        products:[],
        clientsByName:[], //este array es para la busqueda por nombre
        fullName:'', //nombre completo para desplegar los clientes
        clientByPhoneNumber:[], //este array guardara el cliente buscado por telefono
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
        phoneNumber:'',     
        price:'',
        estado:'',               
        dialogProduct:false,
        dialogClient:false
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
      // listar clientes por nombre completo
      listClients(){      
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        let splitedName = this.fullName.split(' ')
        console.log(splitedName)
        axios.get('api/client/ListInSale/',{
          params:{
            'name':splitedName[0],
            'lastName':splitedName[1]
          }
          },headers)
        .then(function (response) {
          // handle success        
          me.clientsByName = response.data                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);   
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }      
        })
      }, 
      // metodo para llarmar al controlador cliente y buscar un cliente por 
      // numero de telefono y luego agregarlo a la venta
      searchByPhoneNumber() {
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/client/getByPhoneNumber/'+this.phoneNumber,headers)
        .then(function (response) {
          // handle success
          console.log(response.data)
          me.addClientSale(response.data)                                                     
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
      // metodo para agregar un cliente a la venta (esto es opcional)
      addClientSale(data = []){      
        if (this.findInClientSale()) 
        {
          alert("ya existe un cliente agregado")
        } else {         
          this.clientByPhoneNumber.push({
            idClient:data['idClient'],
            name:data['name'],
            lastName:data['lastName'],
            documentType:data['documentType'],
            documentNumber:data['documentNumber'],
            phoneNumber:data['phoneNumber'],
           
          })
        }        
      },
      // metodo para agregar productos al detalle  
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
      findInClientSale(){        
        if(this.clientByPhoneNumber.length > 0 ){
          return true
        }                             
        return false
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
        var obj ={
            'IdUser':me.$store.state.user.IdUser,
            'IdClient':me.clientByPhoneNumber[0]['idClient'],            
            'TypeSale':me.typeSale,
            'VoucherSeries':me.voucherSeries,
            'VoucherNumber':me.voucherNumber,            
            'TotalPrice':me.total,           
            'Detail':me.details
          }
          console.log(obj)
        axios.post('api/sale/Create/',
          {
            'IdUser':me.$store.state.user.IdUser,
            'IdClient':me.clientByPhoneNumber[0]['idClient'],            
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
        // this.$emit('save',{ 
          
        // })
        this.addNewSale()
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
