<template>
  <v-container>
    <v-layout>
      <v-card>
        <v-card-title>
          Proveedores
          <div class="flex-grow-1"></div>
          <v-text-field
            v-model="search"
            append-icon="search"
            label="Buscar"
            single-line
            hide-details
          ></v-text-field>
          <v-spacer></v-spacer>
 
          <v-dialog v-model="dialogNewProvider" persistent max-width="600px">
            <template v-slot:activator="{ on }">
              <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo Proveedor</v-btn>
            </template>
              <FormNewProvider                                 
                @save="addNewProvider"
                @cancel="closeDialog"
              />       
          </v-dialog>         
        </v-card-title>                       
       
        <v-data-table
          :headers="headersDataTable"
          :items="providers"
          :search="search"
        >
          <template v-slot:item.condition="{item}">                
              <div v-if="item.condition">
                <span class="blue--text">Activo</span>
              </div>
              <div v-else>
                <span class="red--text">Inactivo</span>
              </div>
          </template>
          
          <template v-slot:item.action="{ item }">         
            <v-icon
              small
              class="mr-2"                  
              @click="showProviderforEdit(item)"
            >
              edit
            </v-icon>                                                                
            <template>
              <v-icon
              small
              @click=""
            >
              delete_sweep
            </v-icon>
            </template>            
          </template>  

        </v-data-table>
      </v-card>

      <template>            
        <v-dialog v-model="dialogEdit" max-width="600px">       
          <FormEditProvider
            :provider="provider"                     
            @save="updateProvider"
            @cancel="closeDialog"
          />  
                      
        </v-dialog>                              
      </template>

    </v-layout>
  </v-container>
</template>

<script>
  import axios from 'axios'
  //importar formularios de editar y agregar
  import FormNewProvider from '@/components/provider/FormNewProvider'
  import FormEditProvider from '@/components/provider/FormEditProvider'
  
  export default {
    components: {
      FormNewProvider,
      FormEditProvider
    },
    data () {
      return {        
        headersDataTable:[
          {text:'ID', value:'idProvider'},
          {text:'Nombre', value:'providerName'},
          {text:'Numero de documento', value:'documentNumber'},         
          {text:'Direccion', value:'address'},
          {text:'Numero de telefono', value:'phoneNumber'},     
          {text:'Email', value:'email'},     
          {text: 'Actions', value: 'action', sortable: false }
        ],
        providers:[],        
        provider:'',        
        search:'',   
        dialogNewProvider:false,
        dialogEdit:false,
        adModal:0,  //activar o desactivar el modal      
        adAction:0,
        adName:'',
        adId:''
      }
    },
    created () {
      this.listProviders();
    },
    methods: {
      listProviders() {
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/provider/list',headers)
        .then(function (response) {
          // handle success
          me.providers = response.data      
          console.log(me.labs)                                              
          })
        .catch(function (error) {
          // handle error          
          console.log(error);   
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }      
        })
      },
      addNewProvider(provider){   
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.post('api/provider/Create/',
        {                
          'ProviderName':provider.providerName,
          'DocumentNumber':provider.documentNumber, 
          'Address':provider.address,
          'PhoneNumber':provider.phoneNumber,
          'Email':provider.email
        },
          headers)
        .then(function (response) {
          // handle success
          console.log('success') 
          me.listProviders()
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
      showProviderforEdit(item){                      
        this.provider = item       
        this.dialogEdit = true      
      },
      updateProvider(item){
        this.provider = item.provider
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}              
        axios.put('api/provider/update',
        {
          'IdProvider':me.provider.idProvider,
          'ProviderName':me.provider.providerName,
          'DocumentNumber':me.provider.documentNumber, 
          'Address':me.provider.address,
          'PhoneNumber':me.provider.phoneNumber,
          'Email':me.provider.email
        },headers)
        .then(function (response) {
          // handle success
          console.log('Update Successful')       
          me.closeDialog()     
          me.listProviders()                                                                      
        })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      activateDeactivateShowProvider(action, item){
       
      },        
      closeDialog(switchBool){
        this.dialogEdit = switchBool
        this.dialogNewProvider = switchBool
      },
    },
  }
</script>